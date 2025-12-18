using Hits.Blazor.Todo.FinalProject.GubanovaSO.Data;
using Hits.Blazor.Todo.FinalProject.GubanovaSO.Models;
using Microsoft.EntityFrameworkCore;

namespace Hits.Blazor.Todo.FinalProject.GubanovaSO.Data.Services
{
    public class CourseService
    {
        private readonly EducationDbContext _context;

        public CourseService(EducationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses
                .Include(c => c.Enrollments)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateCourseAsync(Course course)
        {
            course.CreatedDate = DateTime.UtcNow;
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            var existing = await _context.Courses.FindAsync(course.Id);
            if (existing != null)
            {
                existing.Title = course.Title;
                existing.Description = course.Description;
                existing.Content = course.Content;
                existing.Category = course.Category;
                existing.DurationHours = course.DurationHours;
                existing.DifficultyLevel = course.DifficultyLevel;
                existing.UpdatedDate = DateTime.UtcNow;

                _context.Courses.Update(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCourseCountAsync()
        {
            return await _context.Courses.CountAsync();
        }

        public async Task<List<Course>> GetCoursesByCategoryAsync(string category)
        {
            return await _context.Courses
                .Where(c => c.Category == category)
                .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByInstructorAsync(string instructorId)
        {
            return await _context.Courses
                .Where(c => c.InstructorId == instructorId)
                .ToListAsync();
        }
    }
}