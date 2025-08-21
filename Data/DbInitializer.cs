using AcademIQ_LMS.Models;
using System.Security.Cryptography;
using System.Text;

namespace AcademIQ_LMS.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if users already exist
            if (context.Users.Any())
            {
                return; // Database has already been seeded
            }

            // Add sample users
            var users = new User[]
            {
                new User
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@academiq.com",
                    Username = "admin",
                    Password = HashPassword("admin123"),
                    Role = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new User
                {
                    FirstName = "John",
                    LastName = "Teacher",
                    Email = "teacher@academiq.com",
                    Username = "teacher",
                    Password = HashPassword("teacher123"),
                    Role = UserRole.Teacher,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new User
                {
                    FirstName = "Sarah",
                    LastName = "Student",
                    Email = "student@academiq.com",
                    Username = "student",
                    Password = HashPassword("student123"),
                    Role = UserRole.Student,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            // Add sample courses
            var teacher = context.Users.First(u => u.Role == UserRole.Teacher);
            var courses = new Course[]
            {
                new Course
                {
                    Title = "Introduction to Programming",
                    Description = "Learn the basics of programming with C# and .NET",
                    TeacherId = teacher.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Course
                {
                    Title = "Web Development Fundamentals",
                    Description = "Master HTML, CSS, and JavaScript for web development",
                    TeacherId = teacher.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            // Add sample enrollments
            var student = context.Users.First(u => u.Role == UserRole.Student);
            var enrollments = new Enrollment[]
            {
                new Enrollment
                {
                    StudentId = student.Id,
                    CourseId = courses[0].Id,
                    EnrollmentDate = DateTime.UtcNow,
                    IsActive = true
                }
            };

            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();

            // Add sample content
            var content = new Content[]
            {
                new Content
                {
                    Title = "Programming Basics PDF",
                    Description = "Introduction to programming concepts and C# syntax",
                    Type = ContentType.PDF,
                    FilePath = "/uploads/sample.pdf",
                    FileName = "programming_basics.pdf",
                    FileSize = 1024000,
                    CourseId = courses[0].Id,
                    UploadedById = teacher.Id,
                    UploadedAt = DateTime.UtcNow,
                    IsActive = true
                },
                new Content
                {
                    Title = "Web Development Guide",
                    Description = "Complete guide to HTML, CSS, and JavaScript",
                    Type = ContentType.Document,
                    FilePath = "/uploads/sample.docx",
                    FileName = "web_development_guide.docx",
                    FileSize = 2048000,
                    CourseId = courses[1].Id,
                    UploadedById = teacher.Id,
                    UploadedAt = DateTime.UtcNow,
                    IsActive = true
                }
            };

            context.Contents.AddRange(content);
            context.SaveChanges();

            // Add sample quizzes
            var quizzes = new Quiz[]
            {
                new Quiz
                {
                    Title = "Programming Fundamentals Quiz",
                    Description = "Test your knowledge of basic programming concepts",
                    CourseId = courses[0].Id,
                    CreatedById = teacher.Id,
                    CreatedAt = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(7),
                    TimeLimitMinutes = 30,
                    IsActive = true
                }
            };

            context.Quizzes.AddRange(quizzes);
            context.SaveChanges();

            // Add sample quiz questions
            var questions = new QuizQuestion[]
            {
                new QuizQuestion
                {
                    QuestionText = "What is the correct syntax for declaring a variable in C#?",
                    Type = QuestionType.MultipleChoice,
                    QuizId = quizzes[0].Id,
                    Points = 5,
                    Order = 1,
                    IsActive = true
                },
                new QuizQuestion
                {
                    QuestionText = "Is C# an object-oriented programming language?",
                    Type = QuestionType.TrueFalse,
                    QuizId = quizzes[0].Id,
                    Points = 3,
                    Order = 2,
                    IsActive = true
                }
            };

            context.QuizQuestions.AddRange(questions);
            context.SaveChanges();

            // Add sample quiz options
            var options = new QuizOption[]
            {
                new QuizOption
                {
                    OptionText = "var name = value;",
                    IsCorrect = true,
                    QuestionId = questions[0].Id,
                    Order = 1,
                    IsActive = true
                },
                new QuizOption
                {
                    OptionText = "variable name = value;",
                    IsCorrect = false,
                    QuestionId = questions[0].Id,
                    Order = 2,
                    IsActive = true
                },
                new QuizOption
                {
                    OptionText = "string name = value;",
                    IsCorrect = false,
                    QuestionId = questions[0].Id,
                    Order = 3,
                    IsActive = true
                },
                new QuizOption
                {
                    OptionText = "True",
                    IsCorrect = true,
                    QuestionId = questions[1].Id,
                    Order = 1,
                    IsActive = true
                },
                new QuizOption
                {
                    OptionText = "False",
                    IsCorrect = false,
                    QuestionId = questions[1].Id,
                    Order = 2,
                    IsActive = true
                }
            };

            context.QuizOptions.AddRange(options);
            context.SaveChanges();
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
} 