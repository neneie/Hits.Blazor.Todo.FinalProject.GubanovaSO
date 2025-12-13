using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class UserProgress
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public int LessonId { get; set; }

        [ForeignKey(nameof(LessonId))]
        public Lesson Lesson { get; set; } = null!;

        public int EnrollmentId { get; set; }

        [ForeignKey(nameof(EnrollmentId))]
        public Enrollment Enrollment { get; set; } = null!;

        public DateTime StartedDate { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        [Range(0, 100)]
        public int CompletionPercentage { get; set; } = 0;

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
