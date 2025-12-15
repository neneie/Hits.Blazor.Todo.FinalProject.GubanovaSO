using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Models
{
    public class UserProgress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int LessonId { get; set; }

        [ForeignKey(nameof(LessonId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Lesson? Lesson { get; set; }

        [Required]
        public int EnrollmentId { get; set; }

        [ForeignKey(nameof(EnrollmentId))]
        public Enrollment? Enrollment { get; set; }

        public DateTime StartedDate { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; } = false;

        [Range(0, 100)]
        public int CompletionPercentage { get; set; } = 0;

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
