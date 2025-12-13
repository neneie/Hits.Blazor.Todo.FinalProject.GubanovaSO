using System.ComponentModel.DataAnnotations;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название курса обязательно")]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание обязательно")]
        [StringLength(2000, MinimumLength = 10)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Range(1, 3)]
        public int DifficultyLevel { get; set; } = 1;

        public int DurationHours { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        [Required]
        public string InstructorId { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // Навигационные свойства
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Test> Tests { get; set; } = new List<Test>();
    }
}
