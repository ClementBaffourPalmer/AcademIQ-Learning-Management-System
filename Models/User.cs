using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademIQ_LMS.Models
{
    public enum UserRole
    {
        Admin,
        Teacher,
        Student
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Course>? Courses { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
        public virtual ICollection<Quiz>? CreatedQuizzes { get; set; }
        public virtual ICollection<QuizAttempt>? QuizAttempts { get; set; }
    }
} 