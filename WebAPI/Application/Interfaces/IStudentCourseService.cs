using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces
{
    public interface IStudentCourseService
    {
        IEnumerable<StudentCourse> GetAllStudentCourses();
        StudentCourse GetStudentCourseById(int studentId, int courseId);
        void AddStudentCourse(StudentCourse studentCourse);
        void UpdateStudentCourse(StudentCourse studentCourse);
        void DeleteStudentCourse(int studentId, int courseId);
    }
}
