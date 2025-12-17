using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Models;
using Microsoft.EntityFrameworkCore;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Data.Services
{
    public class StatisticsService
    {
        private readonly EducationDbContext _context;

        public StatisticsService(EducationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalCoursesCountAsync()
        {
            return await _context.Courses.Where(c => c.IsActive).CountAsync();
        }

        public async Task<int> GetTotalEnrollmentsAsync()
        {
            return await _context.Enrollments.CountAsync();
        }

        public async Task<int> GetCompletedEnrollmentsAsync()
        {
            return await _context.Enrollments.Where(e => e.IsCompleted).CountAsync();
        }

        public async Task<double> GetAverageCompletionPercentageAsync()
        {
            var enrollments = await _context.Enrollments.ToListAsync();
            if (enrollments.Count == 0) return 0;
            return enrollments.Average(e => e.ProgressPercentage);
        }

        public async Task<int> GetTotalTestsPassedAsync()
        {
            return await _context.TestResults.Where(tr => tr.IsPassed).CountAsync();
        }

        public async Task<double> GetAverageTestScoreAsync()
        {
            var results = await _context.TestResults.ToListAsync();
            if (results.Count == 0) return 0;
            return results.Average(tr => tr.PercentageScore);
        }

        public async Task<List<Course>> GetMostPopularCoursesAsync(int limit = 5)
        {
            return await _context.Courses
                .OrderByDescending(c => c.Enrollments.Count)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetCourseCategoryStatsAsync()
        {
            var stats = await _context.Courses
                .Where(c => c.IsActive)
                .GroupBy(c => c.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Category, x => x.Count);

            return stats;
        }
    }
}
