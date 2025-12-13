using System.ComponentModel.DataAnnotations;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class Achievement
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public string? IconUrl { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
    }
}
