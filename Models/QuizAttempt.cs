using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademIQ_LMS.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public virtual User Student { get; set; } = null!;

        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; } = null!;

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        public int? Score { get; set; }

        public int? MaxScore { get; set; }

        public double? Percentage { get; set; }

        public bool IsCompleted { get; set; } = false;

        // Navigation properties
        public virtual ICollection<QuizAnswer>? Answers { get; set; }
    }
} 