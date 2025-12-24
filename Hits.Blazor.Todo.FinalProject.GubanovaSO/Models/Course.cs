using System.ComponentModel.DataAnnotations;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Название должно быть от 3 до 200 символов")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание обязательно")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Описание должно быть от 10 до 2000 символов")]
        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;


        [Range(1, 3, ErrorMessage = "Выберите уровень сложности")]
        public int DifficultyLevel { get; set; } = 1;

        public int DurationHours { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        [Required]
        public string InstructorId { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<Lesson> Lessons { get; set; } = new  List<Lesson>();

        public string Content { get; set; } = string.Empty;

    }
}