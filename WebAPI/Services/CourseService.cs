using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly List<Course> _courses = new List<Course>();

        public IEnumerable<Course> GetAllCourses() => _courses;

        public Course GetCourseById(int id) => _courses.FirstOrDefault(c => c.Id == id);

        public void AddCourse(Course course) => _courses.Add(course);

        public void UpdateCourse(Course course)
        {
            var existingCourse = GetCourseById(course.Id);
            if (existingCourse != null)
            {
                existingCourse.Name = course.Name;
                existingCourse.Description = course.Description;
            }
        }

        public void DeleteCourse(int id) => _courses.RemoveAll(c => c.Id == id);
    }
}
