using System.ComponentModel.DataAnnotations;

namespace AcademIQ_LMS.Models
{
    public class QuizOption
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public virtual QuizQuestion Question { get; set; } = null!;

        public int Order { get; set; }

        public bool IsActive { get; set; } = true;
    }
} 