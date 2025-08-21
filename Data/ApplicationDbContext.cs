using Microsoft.EntityFrameworkCore;
using AcademIQ_LMS.Models;

namespace AcademIQ_LMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
            });

            // Configure Course entity
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasOne(c => c.Teacher)
                    .WithMany(u => u.Courses)
                    .HasForeignKey(c => c.TeacherId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Enrollment entity
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasOne(e => e.Student)
                    .WithMany(u => u.Enrollments)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.Enrollments)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Ensure unique enrollment per student per course
                entity.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();
            });

            // Configure Content entity
            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasOne(c => c.Course)
                    .WithMany(c => c.Contents)
                    .HasForeignKey(c => c.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.UploadedBy)
                    .WithMany()
                    .HasForeignKey(c => c.UploadedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Quiz entity
            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasOne(q => q.Course)
                    .WithMany(c => c.Quizzes)
                    .HasForeignKey(q => q.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(q => q.CreatedBy)
                    .WithMany(u => u.CreatedQuizzes)
                    .HasForeignKey(q => q.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure QuizQuestion entity
            modelBuilder.Entity<QuizQuestion>(entity =>
            {
                entity.HasOne(q => q.Quiz)
                    .WithMany(q => q.Questions)
                    .HasForeignKey(q => q.QuizId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure QuizOption entity
            modelBuilder.Entity<QuizOption>(entity =>
            {
                entity.HasOne(o => o.Question)
                    .WithMany(q => q.Options)
                    .HasForeignKey(o => o.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure QuizAttempt entity
            modelBuilder.Entity<QuizAttempt>(entity =>
            {
                entity.HasOne(a => a.Student)
                    .WithMany(u => u.QuizAttempts)
                    .HasForeignKey(a => a.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Quiz)
                    .WithMany(q => q.Attempts)
                    .HasForeignKey(a => a.QuizId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure QuizAnswer entity
            modelBuilder.Entity<QuizAnswer>(entity =>
            {
                entity.HasOne(a => a.Attempt)
                    .WithMany(att => att.Answers)
                    .HasForeignKey(a => a.AttemptId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Question)
                    .WithMany(q => q.Answers)
                    .HasForeignKey(a => a.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.SelectedOption)
                    .WithMany()
                    .HasForeignKey(a => a.SelectedOptionId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
} 