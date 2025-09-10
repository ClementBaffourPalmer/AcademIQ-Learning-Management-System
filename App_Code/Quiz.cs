using System;
using System.Collections.Generic;

namespace AcademIQ_LMS.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CreatedByUserId { get; set; }
        public List<QuizQuestion> Questions { get; set; }
    }

    public class QuizQuestion
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string QuestionText { get; set; }
        public List<QuizOption> Options { get; set; }
        public int CorrectOptionId { get; set; }
    }

    public class QuizOption
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
    }

    public class QuizAttempt
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public DateTime AttemptedAt { get; set; }
        public List<QuizResult> Results { get; set; }
    }

    public class QuizResult
    {
        public int Id { get; set; }
        public int AttemptId { get; set; }
        public int QuestionId { get; set; }
        public int SelectedOptionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
