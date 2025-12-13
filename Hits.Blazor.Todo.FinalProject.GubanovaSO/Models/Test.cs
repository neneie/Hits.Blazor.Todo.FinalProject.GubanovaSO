using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class Test
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = null!;

        [Range(0, 100)]
        public int PassingScore { get; set; } = 70;

        public int TimeoutMinutes { get; set; } = 60;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Навигационные свойства
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<TestResult> Results { get; set; } = new List<TestResult>();
    }
}
