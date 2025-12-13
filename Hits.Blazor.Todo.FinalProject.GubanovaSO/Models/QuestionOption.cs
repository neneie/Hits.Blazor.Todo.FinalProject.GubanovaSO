using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class QuestionOption
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; } = false;

        public int OrderNumber { get; set; }
    }
}
