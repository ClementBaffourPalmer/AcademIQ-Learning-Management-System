using System.Data.Entity;
using AcademIQ_LMS.Models;

namespace AcademIQ_LMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            // Configure Course entity
            modelBuilder.Entity<Course>()
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Course>()
                .Property(e => e.Description)
                .IsRequired();

            // Configure Content entity
            modelBuilder.Entity<Content>()
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Content>()
                .Property(e => e.Description)
                .IsRequired();

            modelBuilder.Entity<Content>()
                .Property(e => e.FileName)
                .IsRequired();

            modelBuilder.Entity<Content>()
                .Property(e => e.FilePath)
                .IsRequired();

            // Configure Quiz entity
            modelBuilder.Entity<Quiz>()
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Quiz>()
                .Property(e => e.Description)
                .IsRequired();

            // Configure QuizQuestion entity
            modelBuilder.Entity<QuizQuestion>()
                .Property(e => e.QuestionText)
                .IsRequired();

            // Configure QuizAnswer entity
            modelBuilder.Entity<QuizAnswer>()
                .Property(e => e.AnswerText)
                .IsRequired();
        }
    }
} 