using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;

namespace Tests.Tests
{
    public class StudentServiceTests
    {
        private readonly StudentService _studentService;

        public StudentServiceTests()
        {
            _studentService = new StudentService();
        }

        [Fact]
        public void AddStudent_ShouldAddStudent()
        {
            var student = new Student { Id = 1, FirstName = "John", LastName = "Doe", EnrollmentDate = DateTime.Now };
            _studentService.AddStudent(student);

            var result = _studentService.GetStudentById(1);

            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
        }

        [Fact]
        public void GetAllStudents_ShouldReturnAllStudents()
        {
            _studentService.AddStudent(new Student { Id = 1, FirstName = "John", LastName = "Doe", EnrollmentDate = DateTime.Now });
            _studentService.AddStudent(new Student { Id = 2, FirstName = "Jane", LastName = "Doe", EnrollmentDate = DateTime.Now });

            var result = _studentService.GetAllStudents();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetStudentById_ShouldReturnStudent()
        {
            _studentService.AddStudent(new Student { Id = 1, FirstName = "John", LastName = "Doe", EnrollmentDate = DateTime.Now });

            var result = _studentService.GetStudentById(1);

            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public void UpdateStudent_ShouldUpdateStudent()
        {
            var student = new Student { Id = 1, FirstName = "John", LastName = "Doe", EnrollmentDate = DateTime.Now };
            _studentService.AddStudent(student);

            student.FirstName = "Updated John";
            _studentService.UpdateStudent(student);

            var result = _studentService.GetStudentById(1);

            Assert.NotNull(result);
            Assert.Equal("Updated John", result.FirstName);
        }

        [Fact]
        public void DeleteStudent_ShouldRemoveStudent()
        {
            var student = new Student { Id = 1, FirstName = "John", LastName = "Doe", EnrollmentDate = DateTime.Now };
            _studentService.AddStudent(student);

            _studentService.DeleteStudent(1);

            var result = _studentService.GetStudentById(1);

            Assert.Null(result);
        }
    }
}