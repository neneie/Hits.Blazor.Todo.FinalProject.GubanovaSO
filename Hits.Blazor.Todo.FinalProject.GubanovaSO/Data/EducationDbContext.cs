using Microsoft.EntityFrameworkCore;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Models;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Data
{
    public class EducationDbContext : DbContext
    {
        public EducationDbContext(DbContextOptions<EducationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Индексы
            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.UserId, e.CourseId })
                .IsUnique();

            modelBuilder.Entity<TestResult>()
                .HasIndex(tr => new { tr.UserId, tr.TestId });

            modelBuilder.Entity<UserProgress>()
                .HasIndex(up => new { up.UserId, up.LessonId });

            // Каскадное удаление
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Test>()
                .HasMany(t => t.Questions)
                .WithOne(q => q.Test)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Options)
                .WithOne(qo => qo.Question)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
