using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly List<Student> _students = new List<Student>();

        public IEnumerable<Student> GetAllStudents() => _students;

        public Student GetStudentById(int id) => _students.FirstOrDefault(s => s.Id == id);

        public void AddStudent(Student student) => _students.Add(student);

        public void UpdateStudent(Student student)
        {
            var existingStudent = GetStudentById(student.Id);
            if (existingStudent != null)
            {
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.EnrollmentDate = student.EnrollmentDate;
            }
        }

        public void DeleteStudent(int id) => _students.RemoveAll(s => s.Id == id);
    }
}
