using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string QuestionText { get; set; } = string.Empty;

        public int TestId { get; set; }

        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; } = null!;

        [Range(1, 4)]
        public int QuestionType { get; set; } = 1;

        public int OrderNumber { get; set; }

        [Range(1, 100)]
        public int Weight { get; set; } = 1;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
    }
}
