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
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task<List<Course>> GetCoursesByInstructorAsync(string instructorId)
        {
            if (string.IsNullOrEmpty(instructorId))
            {
                throw new ArgumentException("Идентификатор инструктора не может быть пустым");
            }

            return await _context.Courses
                .Include(c => c.Lessons)
                .Where(c => c.InstructorId == instructorId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<int> CreateCourseAsync(Course course)
        {
            if (string.IsNullOrEmpty(course.Title) || string.IsNullOrEmpty(course.Description))
            {
                throw new ArgumentException("Название и описание курса обязательны");
            }

            course.CreatedDate = DateTime.UtcNow;
            course.IsActive = true;

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course.Id;
        }

        public async Task UpdateCourseAsync(Course course)
        {
            var existingCourse = await _context.Courses.FindAsync(course.Id);
            if (existingCourse == null)
            {
                throw new ArgumentException("Курс не найден");
            }

            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            existingCourse.Category = course.Category;
            existingCourse.ImageUrl = course.ImageUrl;
            existingCourse.DifficultyLevel = course.DifficultyLevel;
            existingCourse.DurationHours = course.DurationHours;
            existingCourse.UpdatedDate = DateTime.UtcNow;

            _context.Courses.Update(existingCourse);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                course.IsActive = false;
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Course>> SearchCoursesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllCoursesAsync();
            }

            var term = searchTerm.ToLower();
            return await _context.Courses
                .Include(c => c.Lessons)
                .Where(c => (c.Title.ToLower().Contains(term) ||
                            c.Description.ToLower().Contains(term) ||
                            c.Category.ToLower().Contains(term)) &&
                       c.IsActive)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<int> GetCourseCountAsync()
        {
            return await _context.Courses
                .Where(c => c.IsActive)
                .CountAsync();
        }

        public async Task<List<Course>> GetPopularCoursesAsync(int count = 6)
        {
            return await _context.Courses
                .Include(c => c.Lessons)
                .Include(c => c.Enrollments)
                .Where(c => c.IsActive)
                .OrderByDescending(c => c.Enrollments.Count)
                .Take(count)
                .ToListAsync();
        }
    }
}
