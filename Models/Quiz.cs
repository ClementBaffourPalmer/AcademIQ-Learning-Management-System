using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademIQ_LMS.Models
{
    /// <summary>
    /// Represents a quiz that belongs to a course.
    /// Quizzes are created by Admins or Teachers and can be taken by Students.
    /// </summary>
    public class Quiz
    {
        /// <summary>
        /// Primary key for the quiz.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the quiz (e.g., "Week 1 Assessment").
        /// </summary>
        [Required(ErrorMessage = "Quiz title is required.")]
        [StringLength(200, ErrorMessage = "Title must be less than 200 characters.")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Optional description to give more context or instructions.
        /// </summary>
        [StringLength(1000, ErrorMessage = "Description must be less than 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key to the course this quiz belongs to.
        /// </summary>
        [Required(ErrorMessage = "Course is required.")]
        public int CourseId { get; set; }

        /// <summary>
        /// Navigation property for the related course.
        /// </summary>
        public virtual Course Course { get; set; } = null!;

        /// <summary>
        /// The ID of the user (Admin or Teacher) who created this quiz.
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// Navigation property for the quiz creator.
        /// </summary>
        public virtual User CreatedBy { get; set; } = null!;

        /// <summary>
        /// Timestamp when the quiz was created (stored in UTC).
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Optional deadline for completing the quiz.
        /// Null means no deadline.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Time limit in minutes. 0 means no time limit.
        /// </summary>
        public int TimeLimitMinutes { get; set; } = 0;

        /// <summary>
        /// Whether the quiz is active and visible to students.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// All questions that belong to this quiz.
        /// </summary>
        public virtual ICollection<QuizQuestion>? Questions { get; set; }

        /// <summary>
        /// All attempts made by students for this quiz.
        /// </summary>
        public virtual ICollection<QuizAttempt>? Attempts { get; set; }

        /// <summary>
        /// All answers provided by students for this quiz.
        /// </summary>
        public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = new List<QuizAnswer>();
    }
}
