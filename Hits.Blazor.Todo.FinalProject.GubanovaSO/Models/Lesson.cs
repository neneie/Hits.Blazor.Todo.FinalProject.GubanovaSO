using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название урока обязательно")]
        [StringLength(250)]
        public string Title { get; set; } = string.Empty;

        [StringLength(3000)]
        public string Content { get; set; } = string.Empty;

        [StringLength(500)]
        public string? VideoUrl { get; set; }

        public int OrderNumber { get; set; }

        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public bool IsPublished { get; set; } = false;

        // Навигационные свойства
        public ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
    }
}
