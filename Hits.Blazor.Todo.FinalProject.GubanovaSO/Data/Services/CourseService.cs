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
                .Include(c => c.Lessons)
                .Where(c => c.IsActive)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Lessons.OrderBy(l => l.OrderNumber))
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Course>> GetCoursesByInstructorAsync(string instructorId)
        {
            return await _context.Courses
                .Where(c => c.InstructorId == instructorId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<int> CreateCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course.Id;
        }

        public async Task UpdateCourseAsync(Course course)
        {
            course.UpdatedDate = DateTime.UtcNow;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
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

        public async Task<List<Course>> SearchCoursesAsync(string searchTerm)
        {
            return await _context.Courses
                .Where(c => c.Title.Contains(searchTerm) || c.Description.Contains(searchTerm))
                .Where(c => c.IsActive)
                .ToListAsync();
        }
    }
}
