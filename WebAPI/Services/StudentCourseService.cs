using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly List<StudentCourse> _studentCourses = new List<StudentCourse>();

        public IEnumerable<StudentCourse> GetAllStudentCourses() => _studentCourses;

        public StudentCourse GetStudentCourseById(int studentId, int courseId) =>
            _studentCourses.FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);

        public void AddStudentCourse(StudentCourse studentCourse) => _studentCourses.Add(studentCourse);

        public void UpdateStudentCourse(StudentCourse studentCourse)
        {
            var existingStudentCourse = GetStudentCourseById(studentCourse.StudentId, studentCourse.CourseId);
            if (existingStudentCourse != null)
            {
                existingStudentCourse.EnrollmentDate = studentCourse.EnrollmentDate;
            }
        }

        public void DeleteStudentCourse(int studentId, int courseId) =>
            _studentCourses.RemoveAll(sc => sc.StudentId == studentId && sc.CourseId == courseId);
    }
}
