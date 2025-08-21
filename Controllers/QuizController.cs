using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademIQ_LMS.Data;
using AcademIQ_LMS.Models;

namespace AcademIQ_LMS.Controllers
{
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? courseId)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetString("UserId");

            if (!int.TryParse(userId, out int userIdInt) || string.IsNullOrEmpty(userRole))
                return RedirectToAction("Login", "Account");

            List<Quiz> quizzes = new();

            if (courseId.HasValue)
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course == null) return NotFound();

                if (userRole == "Teacher" && course.TeacherId != userIdInt) return Forbid();

                if (userRole == "Student")
                {
                    var enrolled = await _context.Enrollments.AnyAsync(e => e.StudentId == userIdInt && e.CourseId == courseId && e.IsActive);
                    if (!enrolled) return Forbid();
                }

                quizzes = await _context.Quizzes
                    .Include(q => q.Course)
                    .Include(q => q.CreatedBy)
                    .Where(q => q.CourseId == courseId && q.IsActive)
                    .ToListAsync();
            }
            else
            {
                quizzes = userRole switch
                {
                    "Admin" => await _context.Quizzes
                        .Include(q => q.Course)
                        .Include(q => q.CreatedBy)
                        .Where(q => q.IsActive)
                        .ToListAsync(),
                    "Teacher" => await _context.Quizzes
                        .Include(q => q.Course)
                        .Include(q => q.CreatedBy)
                        .Where(q => q.Course.TeacherId == userIdInt && q.IsActive)
                        .ToListAsync(),
                    "Student" => await _context.Quizzes
                        .Include(q => q.Course)
                        .Include(q => q.CreatedBy)
                        .Where(q => q.Course.Enrollments != null && 
                                   q.Course.Enrollments.Any(e => e.StudentId == userIdInt && e.IsActive) && 
                                   q.IsActive)
                        .ToListAsync(),
                    _ => quizzes
                };
            }

            return View(quizzes);
        }

        public async Task<IActionResult> Create(int? courseId)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetString("UserId");

            if (userRole != "Admin" && userRole != "Teacher") return Forbid();

            if (courseId.HasValue)
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course == null || (userRole == "Teacher" && course.TeacherId != int.Parse(userId ?? "0"))) return Forbid();

                ViewBag.CourseId = courseId;
                ViewBag.CourseTitle = course.Title;
            }
            else
            {
                // Populate course dropdown based on user role
                var userIdInt = int.Parse(userId ?? "0");
                var availableCourses = userRole switch
                {
                    "Admin" => await _context.Courses
                        .Include(c => c.Teacher)
                        .Where(c => c.IsActive)
                        .OrderBy(c => c.Title)
                        .ToListAsync(),
                    "Teacher" => await _context.Courses
                        .Include(c => c.Teacher)
                        .Where(c => c.TeacherId == userIdInt && c.IsActive)
                        .OrderBy(c => c.Title)
                        .ToListAsync(),
                    _ => new List<Course>()
                };

                ViewBag.AvailableCourses = availableCourses;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,CourseId,DueDate,TimeLimitMinutes")] Quiz quiz)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (!int.TryParse(userId, out int userIdInt)) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                quiz.CreatedById = userIdInt;
                quiz.CreatedAt = DateTime.UtcNow;
                quiz.IsActive = true;
                _context.Add(quiz);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Quiz created successfully!";
                return RedirectToAction(nameof(Index), new { courseId = quiz.CourseId });
            }

            return View(quiz);
        }

        public async Task<IActionResult> Take(int? id)
        {
            if (id == null) return NotFound();

            var userId = HttpContext.Session.GetString("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");

            if (!int.TryParse(userId, out int studentId) || userRole != "Student")
                return RedirectToAction("Login", "Account");

            var quiz = await _context.Quizzes
                .Include(q => q.Course)
                .Include(q => q.Questions!).ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(q => q.Id == id && q.IsActive);

            if (quiz == null) return NotFound();

            // Check if student is enrolled in the course
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == quiz.CourseId && e.IsActive);
            
            if (enrollment == null)
            {
                TempData["ErrorMessage"] = "You must be enrolled in this course to take the quiz.";
                return RedirectToAction(nameof(Index), new { courseId = quiz.CourseId });
            }

            // Check if student has already attempted this quiz
            var existingAttempt = await _context.QuizAttempts
                .FirstOrDefaultAsync(a => a.StudentId == studentId && a.QuizId == id);
            
            if (existingAttempt != null)
            {
                TempData["ErrorMessage"] = "You have already attempted this quiz.";
                return RedirectToAction(nameof(Index), new { courseId = quiz.CourseId });
            }

            // Check if quiz has questions
            if (quiz.Questions == null || !quiz.Questions.Any())
            {
                TempData["ErrorMessage"] = "This quiz has no questions available.";
                return RedirectToAction(nameof(Index), new { courseId = quiz.CourseId });
            }

            return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(int quizId, Dictionary<int, object> answers)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (!int.TryParse(userId, out int studentId)) return RedirectToAction("Login", "Account");

            var quiz = await _context.Quizzes.Include(q => q.Questions!).ThenInclude(q => q.Options).FirstOrDefaultAsync(q => q.Id == quizId);
            if (quiz == null) return NotFound();

            if (quiz.DueDate.HasValue && DateTime.UtcNow > quiz.DueDate.Value)
            {
                TempData["ErrorMessage"] = "Quiz submission failed. The due date has passed.";
                return RedirectToAction(nameof(Index), new { courseId = quiz.CourseId });
            }

            var attempt = new QuizAttempt
            {
                StudentId = studentId,
                QuizId = quizId,
                StartedAt = DateTime.UtcNow,
                IsCompleted = false
            };

            _context.Add(attempt);
            await _context.SaveChangesAsync();

            int totalScore = 0;
            int maxScore = quiz.Questions?.Sum(q => q.Points) ?? 0;

            foreach (var answer in answers)
            {
                var question = quiz.Questions?.FirstOrDefault(q => q.Id == answer.Key);
                if (question == null) continue;

                var quizAnswer = new QuizAnswer
                {
                    AttemptId = attempt.Id,
                    QuestionId = answer.Key,
                    AnsweredAt = DateTime.UtcNow
                };

                switch (question.Type)
                {
                    case QuestionType.MultipleChoice:
                    case QuestionType.TrueFalse:
                        if (int.TryParse(answer.Value.ToString(), out int optionId))
                        {
                            var selectedOption = question.Options?.FirstOrDefault(o => o.Id == optionId);
                            if (selectedOption != null)
                            {
                                var option = selectedOption;
                                quizAnswer.SelectedOptionId = optionId;
                                quizAnswer.IsCorrect = option.IsCorrect;
                                quizAnswer.PointsEarned = option.IsCorrect ? question.Points : 0;
                                totalScore += quizAnswer.PointsEarned ?? 0;
                            }
                        }
                        break;
                    case QuestionType.ShortAnswer:
                    case QuestionType.Essay:
                        quizAnswer.AnswerText = answer.Value?.ToString()?.Trim();
                        quizAnswer.PointsEarned = 0;
                        break;
                }

                _context.Add(quizAnswer);
            }

            attempt.IsCompleted = true;
            attempt.CompletedAt = DateTime.UtcNow;
            attempt.Score = totalScore;
            attempt.MaxScore = maxScore;
            attempt.Percentage = maxScore > 0 ? (double)totalScore / maxScore * 100 : 0;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Quiz completed! Your score: {totalScore}/{maxScore} ({attempt.Percentage:F1}%)";
            return RedirectToAction(nameof(Results), new { attemptId = attempt.Id });
        }

        public async Task<IActionResult> Results(int? attemptId)
        {
            if (attemptId == null) return NotFound();

            var userId = HttpContext.Session.GetString("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");

            if (!int.TryParse(userId, out int userIdInt)) return RedirectToAction("Login", "Account");

            var attempt = await _context.QuizAttempts
                .Include(a => a.Quiz)
                .Include(a => a.Answers!).ThenInclude(ans => ans.Question)
                .Include(a => a.Answers!).ThenInclude(ans => ans.SelectedOption)
                .FirstOrDefaultAsync(a => a.Id == attemptId);

            if (attempt == null) return NotFound();
            if (userRole != "Admin" && attempt.StudentId != userIdInt) return Forbid();

            return View(attempt);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var quiz = await _context.Quizzes
                .Include(q => q.Course)
                .FirstOrDefaultAsync(q => q.Id == id && q.IsActive);

            if (quiz == null) return NotFound();

            var userRole = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Account");

            var userIdInt = int.Parse(userId ?? "0");

            // Check if user can edit this quiz
            if (userRole != "Admin" && quiz.CreatedById != userIdInt) return Forbid();

            // Populate course dropdown
            var availableCourses = userRole switch
            {
                "Admin" => await _context.Courses
                    .Include(c => c.Teacher)
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.Title)
                    .ToListAsync(),
                "Teacher" => await _context.Courses
                    .Include(c => c.Teacher)
                    .Where(c => c.TeacherId == userIdInt && c.IsActive)
                    .OrderBy(c => c.Title)
                    .ToListAsync(),
                _ => new List<Course>()
            };

            ViewBag.AvailableCourses = availableCourses;

            return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CourseId,DueDate,TimeLimitMinutes")] Quiz quiz)
        {
            if (id != quiz.Id) return NotFound();

            var existingQuiz = await _context.Quizzes.FindAsync(id);
            if (existingQuiz == null) return NotFound();

            var userRole = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Account");

            var userIdInt = int.Parse(userId ?? "0");

            // Check if user can edit this quiz
            if (userRole != "Admin" && existingQuiz.CreatedById != userIdInt) return Forbid();

            if (ModelState.IsValid)
            {
                try
                {
                    existingQuiz.Title = quiz.Title;
                    existingQuiz.Description = quiz.Description;
                    existingQuiz.CourseId = quiz.CourseId;
                    existingQuiz.DueDate = quiz.DueDate;
                    existingQuiz.TimeLimitMinutes = quiz.TimeLimitMinutes;

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Quiz updated successfully!";
                    return RedirectToAction(nameof(Index), new { courseId = quiz.CourseId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // If we get here, there was a validation error
            var availableCourses = userRole switch
            {
                "Admin" => await _context.Courses
                    .Include(c => c.Teacher)
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.Title)
                    .ToListAsync(),
                "Teacher" => await _context.Courses
                    .Include(c => c.Teacher)
                    .Where(c => c.TeacherId == userIdInt && c.IsActive)
                    .OrderBy(c => c.Title)
                    .ToListAsync(),
                _ => new List<Course>()
            };

            ViewBag.AvailableCourses = availableCourses;

            return View(quiz);
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Attempts(int? quizId)
        {
            if (quizId == null) return NotFound();

            var userRole = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userRole) || string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Account");

            if (userRole != "Admin" && userRole != "Teacher") return Forbid();

            var attempts = await _context.QuizAttempts
                .Include(a => a.Student)
                .Include(a => a.Quiz)
                .Where(a => a.QuizId == quizId)
                .ToListAsync();

            return View(attempts);
        }
    }
}
