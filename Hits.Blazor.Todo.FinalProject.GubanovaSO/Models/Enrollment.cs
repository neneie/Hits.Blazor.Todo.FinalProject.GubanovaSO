using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int CourseId { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public DateTime? CompletionDate { get; set; }

        [Range(0, 100)]
        public int ProgressPercentage { get; set; } = 0;

        public bool IsCompleted { get; set; } = false;

        [JsonIgnore]
        public Course? Course { get; set; }
    }
}
