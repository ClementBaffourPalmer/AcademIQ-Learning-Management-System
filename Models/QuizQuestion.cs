using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademIQ_LMS.Models
{
    public enum QuestionType
    {
        MultipleChoice,
        TrueFalse,
        ShortAnswer,
        Essay
    }

    public class QuizQuestion
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string QuestionText { get; set; } = string.Empty;

        public QuestionType Type { get; set; }

        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; } = null!;

        public int Points { get; set; } = 1;

        public int Order { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<QuizOption>? Options { get; set; }
        public virtual ICollection<QuizAnswer>? Answers { get; set; }
    }
} 