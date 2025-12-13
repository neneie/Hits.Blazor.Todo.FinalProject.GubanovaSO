using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class UserAchievement
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public int AchievementId { get; set; }

        [ForeignKey(nameof(AchievementId))]
        public Achievement Achievement { get; set; } = null!;

        public DateTime UnlockedDate { get; set; } = DateTime.UtcNow;
    }
}
