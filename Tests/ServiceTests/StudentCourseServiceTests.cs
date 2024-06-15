using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;

namespace Tests.Tests
{
    public class StudentCourseServiceTests
    {
        private readonly StudentCourseService _studentCourseService;

        public StudentCourseServiceTests()
        {
            _studentCourseService = new StudentCourseService();
        }

        [Fact]
        public void AddStudentCourse_ShouldAddStudentCourse()
        {
            var studentCourse = new StudentCourse { StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.Now };
            _studentCourseService.AddStudentCourse(studentCourse);

            var result = _studentCourseService.GetStudentCourseById(1, 1);

            Assert.NotNull(result);
            Assert.Equal(1, result.StudentId);
            Assert.Equal(1, result.CourseId);
        }

        [Fact]
        public void GetAllStudentCourses_ShouldReturnAllStudentCourses()
        {
            _studentCourseService.AddStudentCourse(new StudentCourse { StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.Now });
            _studentCourseService.AddStudentCourse(new StudentCourse { StudentId = 2, CourseId = 2, EnrollmentDate = DateTime.Now });

            var result = _studentCourseService.GetAllStudentCourses();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetStudentCourseById_ShouldReturnStudentCourse()
        {
            _studentCourseService.AddStudentCourse(new StudentCourse { StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.Now });

            var result = _studentCourseService.GetStudentCourseById(1, 1);

            Assert.NotNull(result);
            Assert.Equal(1, result.StudentId);
            Assert.Equal(1, result.CourseId);
        }

        [Fact]
        public void UpdateStudentCourse_ShouldUpdateStudentCourse()
        {
            var studentCourse = new StudentCourse { StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.Now };
            _studentCourseService.AddStudentCourse(studentCourse);

            var newEnrollmentDate = DateTime.Now.AddDays(1);
            studentCourse.EnrollmentDate = newEnrollmentDate;
            _studentCourseService.UpdateStudentCourse(studentCourse);

            var result = _studentCourseService.GetStudentCourseById(1, 1);

            Assert.NotNull(result);
            Assert.Equal(newEnrollmentDate, result.EnrollmentDate);
        }

        [Fact]
        public void DeleteStudentCourse_ShouldRemoveStudentCourse()
        {
            var studentCourse = new StudentCourse { StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.Now };
            _studentCourseService.AddStudentCourse(studentCourse);

            _studentCourseService.DeleteStudentCourse(1, 1);

            var result = _studentCourseService.GetStudentCourseById(1, 1);

            Assert.Null(result);
        }
    }
}