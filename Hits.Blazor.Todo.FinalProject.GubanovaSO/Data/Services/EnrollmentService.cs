using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Models;
using Microsoft.EntityFrameworkCore;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Data.Services
{
    public class EnrollmentService
    {
        private readonly EducationDbContext _context;

        public EnrollmentService(EducationDbContext context)
        {
            _context = context;
        }

        public async Task<Enrollment?> GetEnrollmentAsync(string userId, int courseId)
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);
        }

        public async Task<List<Enrollment>> GetUserEnrollmentsAsync(string userId)
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.EnrollmentDate)
                .ToListAsync();
        }

        public async Task<int> EnrollUserAsync(string userId, int courseId)
        {
            var existingEnrollment = await GetEnrollmentAsync(userId, courseId);
            if (existingEnrollment != null)
            {
                return existingEnrollment.Id;
            }

            var enrollment = new Enrollment
            {
                UserId = userId,
                CourseId = courseId,
                EnrollmentDate = DateTime.UtcNow,
                ProgressPercentage = 0,
                IsCompleted = false
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment.Id;
        }

        public async Task UpdateProgressAsync(int enrollmentId)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.UserProgresses)
                .FirstOrDefaultAsync(e => e.Id == enrollmentId);

            if (enrollment != null)
            {
                var totalLessons = await _context.Lessons
                    .Where(l => l.CourseId == enrollment.CourseId)
                    .CountAsync();

                if (totalLessons > 0)
                {
                    var completedLessons = enrollment.UserProgresses
                        .Where(up => up.IsCompleted)
                        .Count();

                    enrollment.ProgressPercentage = (completedLessons * 100) / totalLessons;
                }

                _context.Enrollments.Update(enrollment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompleteEnrollmentAsync(int enrollmentId)
        {
            var enrollment = await _context.Enrollments.FindAsync(enrollmentId);
            if (enrollment != null)
            {
                enrollment.IsCompleted = true;
                enrollment.CompletionDate = DateTime.UtcNow;
                enrollment.ProgressPercentage = 100;

                _context.Enrollments.Update(enrollment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
