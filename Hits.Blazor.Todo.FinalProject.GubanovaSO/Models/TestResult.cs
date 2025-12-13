using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class TestResult
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public int TestId { get; set; }

        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; } = null!;

        public int EnrollmentId { get; set; }

        [ForeignKey(nameof(EnrollmentId))]
        public Enrollment Enrollment { get; set; } = null!;

        [Range(0, 100)]
        public int Score { get; set; }

        [Range(0, 100)]
        public int PercentageScore { get; set; }

        public bool IsPassed { get; set; }

        public DateTime CompletedDate { get; set; } = DateTime.UtcNow;

        public int TotalQuestions { get; set; }

        public int CorrectAnswers { get; set; }

        public int TimeSpentSeconds { get; set; }
    }
}
