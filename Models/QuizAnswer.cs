using System.ComponentModel.DataAnnotations;

namespace AcademIQ_LMS.Models
{
    public class QuizAnswer
    {
        public int Id { get; set; }

        public int AttemptId { get; set; }
        public virtual QuizAttempt Attempt { get; set; } = null!;

        public int QuestionId { get; set; }
        public virtual QuizQuestion Question { get; set; } = null!;

        [StringLength(2000)]
        public string? AnswerText { get; set; }

        public int? SelectedOptionId { get; set; }
        public virtual QuizOption? SelectedOption { get; set; }

        public int? PointsEarned { get; set; }

        public bool IsCorrect { get; set; }

        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
    }
} 