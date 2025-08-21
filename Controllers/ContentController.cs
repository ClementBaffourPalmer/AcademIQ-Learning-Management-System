using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademIQ_LMS.Data;
using AcademIQ_LMS.Models;

namespace AcademIQ_LMS.Controllers
{
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContentController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Content
        public async Task<IActionResult> Index(int? courseId)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userRole) || string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var contents = new List<Content>();

            if (courseId.HasValue)
            {
                // Check if user has access to this course
                var course = await _context.Courses.FindAsync(courseId);
                if (course == null)
                {
                    return NotFound();
                }

                var userIdInt = int.Parse(userId);

                switch (userRole)
                {
                    case "Admin":
                        // Admin can see all content
                        break;
                    case "Teacher":
                        // Teacher can only see content for their courses
                        if (course.TeacherId != userIdInt)
                        {
                            return Forbid();
                        }
                        break;
                    case "Student":
                        // Student can only see content for courses they're enrolled in
                        var enrollment = await _context.Enrollments
                            .FirstOrDefaultAsync(e => e.StudentId == userIdInt && e.CourseId == courseId && e.IsActive);
                        if (enrollment == null)
                        {
                            return Forbid();
                        }
                        break;
                }

                contents = await _context.Contents
                    .Include(c => c.Course)
                    .Include(c => c.UploadedBy)
                    .Where(c => c.CourseId == courseId && c.IsActive)
                    .ToListAsync();
            }
            else
            {
                // Show all content based on user role
                switch (userRole)
                {
                    case "Admin":
                        contents = await _context.Contents
                            .Include(c => c.Course)
                            .Include(c => c.UploadedBy)
                            .Where(c => c.IsActive)
                            .ToListAsync();
                        break;
                    case "Teacher":
                        var teacherId = int.Parse(userId);
                        contents = await _context.Contents
                            .Include(c => c.Course)
                            .Include(c => c.UploadedBy)
                            .Where(c => c.Course.TeacherId == teacherId && c.IsActive)
                            .ToListAsync();
                        break;
                    case "Student":
                        var studentId = int.Parse(userId);
                        contents = await _context.Contents
                            .Include(c => c.Course)
                            .Include(c => c.UploadedBy)
                            .Where(c => c.Course.Enrollments!.Any(e => e.StudentId == studentId && e.IsActive) && c.IsActive)
                            .ToListAsync();
                        break;
                }
            }

            return View(contents);
        }

        // GET: Content/Create
        public async Task<IActionResult> Create(int? courseId)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin" && userRole != "Teacher")
            {
                return Forbid();
            }

            if (courseId.HasValue)
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course == null)
                {
                    return NotFound();
                }

                // Check if teacher owns this course
                if (userRole == "Teacher")
                {
                    var userId = HttpContext.Session.GetString("UserId");
                    if (course.TeacherId != int.Parse(userId ?? "0"))
                    {
                        return Forbid();
                    }
                }

                ViewBag.CourseId = courseId;
                ViewBag.CourseTitle = course.Title;
            }
            else
            {
                // Populate course dropdown based on user role
                var userId = HttpContext.Session.GetString("UserId");
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

        // POST: Content/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Type,CourseId")] Content content, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Account");
                }

                content.UploadedById = int.Parse(userId ?? "0");
                content.UploadedAt = DateTime.UtcNow;
                content.IsActive = true;

                if (file != null && file.Length > 0)
                {
                    // Create uploads directory if it doesn't exist
                    var uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    // Generate unique filename
                    var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploadsDir, fileName);

                    // Save file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    content.FileName = file.FileName;
                    content.FilePath = "/uploads/" + fileName;
                    content.FileSize = file.Length;
                }

                _context.Add(content);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Content uploaded successfully!";
                return RedirectToAction(nameof(Index), new { courseId = content.CourseId });
            }

            // If ModelState is not valid, re-populate necessary ViewBag data
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole == "Admin" || userRole == "Teacher")
            {
                if (content.CourseId != null)
                {
                    var course = await _context.Courses.FindAsync(content.CourseId);
                    if (course != null)
                    {
                        ViewBag.CourseId = content.CourseId;
                        ViewBag.CourseTitle = course.Title;
                    }
                }
                else
                {
                    var userId = HttpContext.Session.GetString("UserId");
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
            }

            return View(content);
        }

        // GET: Content/Download/5
        public async Task<IActionResult> Download(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Contents
                .Include(c => c.Course)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);

            if (content == null)
            {
                return NotFound();
            }

            // Check if user has access to this content
            var userRole = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userRole) || string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var userIdInt = int.Parse(userId);

            switch (userRole)
            {
                case "Admin":
                    // Admin can download all content
                    break;
                case "Teacher":
                    // Teacher can only download content from their courses
                    if (content.Course.TeacherId != userIdInt)
                    {
                        return Forbid();
                    }
                    break;
                case "Student":
                    // Student can only download content from courses they're enrolled in
                    var enrollment = await _context.Enrollments
                        .FirstOrDefaultAsync(e => e.StudentId == userIdInt && e.CourseId == content.CourseId && e.IsActive);
                    if (enrollment == null)
                    {
                        return Forbid();
                    }
                    break;
            }

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, content.FilePath.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, "application/octet-stream", content.FileName);
        }

        // POST: Content/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var content = await _context.Contents
                .Include(c => c.Course)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (content == null)
            {
                return NotFound();
            }

            // Check if user can delete this content
            var userRole = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var userIdInt = int.Parse(userId);

            if (userRole != "Admin" && content.UploadedById != userIdInt)
            {
                return Forbid();
            }

            // Soft delete
            content.IsActive = false;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Content deleted successfully!";
            return RedirectToAction(nameof(Index), new { courseId = content.CourseId });
        }
    }
} 