using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademIQ_LMS.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int TeacherId { get; set; }
        public virtual User Teacher { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
        public virtual ICollection<Content>? Contents { get; set; }
        public virtual ICollection<Quiz>? Quizzes { get; set; }
    }
} 
