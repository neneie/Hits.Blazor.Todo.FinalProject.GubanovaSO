using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = string.Empty;

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public DateTime? CompletionDate { get; set; }

        [Range(0, 100)]
        public int ProgressPercentage { get; set; } = 0;

        public bool IsCompleted { get; set; } = false;

        // Навигационные свойства
        public ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
    }
}
