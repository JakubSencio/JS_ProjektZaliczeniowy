using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;
using WebAPI.Presentation.Controllers;

namespace Tests.ServiceTests
{
    public class StudentCoursesControllerTests
    {
        private readonly Mock<IStudentCourseService> _mockStudentCourseService;
        private readonly StudentCoursesController _controller;

        public StudentCoursesControllerTests()
        {
            _mockStudentCourseService = new Mock<IStudentCourseService>();
            _controller = new StudentCoursesController(_mockStudentCourseService.Object);
        }

        [Fact]
        public void GetAllStudentCourses_ReturnsOkResult_WithListOfStudentCourses()
        {
            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse { StudentId = 1, CourseId = 1, EnrollmentDate = System.DateTime.Now },
                new StudentCourse { StudentId = 2, CourseId = 2, EnrollmentDate = System.DateTime.Now }
            };
            _mockStudentCourseService.Setup(service => service.GetAllStudentCourses()).Returns(studentCourses);

            var result = _controller.GetAllStudentCourses();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnStudentCourses = Assert.IsType<List<StudentCourse>>(okResult.Value);
            Assert.Equal(2, returnStudentCourses.Count);
        }

        [Fact]
        public void GetStudentCourseById_ReturnsOkResult_WithStudentCourse()
        {
            var studentCourse = new StudentCourse { StudentId = 1, CourseId = 1, EnrollmentDate = System.DateTime.Now };
            _mockStudentCourseService.Setup(service => service.GetStudentCourseById(1, 1)).Returns(studentCourse);

            var result = _controller.GetStudentCourseById(1, 1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnStudentCourse = Assert.IsType<StudentCourse>(okResult.Value);
            Assert.Equal(1, returnStudentCourse.StudentId);
        }

        [Fact]
        public void GetStudentCourseById_ReturnsNotFoundResult_WhenStudentCourseNotExists()
        {
            _mockStudentCourseService.Setup(service => service.GetStudentCourseById(1, 1)).Returns((StudentCourse)null);

            var result = _controller.GetStudentCourseById(1, 1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddStudentCourse_ReturnsCreatedAtActionResult_WithStudentCourse()
        {
            var studentCourse = new StudentCourse { StudentId = 1, CourseId = 1, EnrollmentDate = System.DateTime.Now };

            var result = _controller.AddStudentCourse(studentCourse);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnStudentCourse = Assert.IsType<StudentCourse>(createdAtActionResult.Value);
            Assert.Equal(1, returnStudentCourse.StudentId);
        }

        [Fact]
        public void UpdateStudentCourse_ReturnsNoContentResult()
        {
            var studentCourse = new StudentCourse { StudentId = 1, CourseId = 1, EnrollmentDate = System.DateTime.Now };

            var result = _controller.UpdateStudentCourse(1, 1, studentCourse);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateStudentCourse_ReturnsBadRequestResult_WhenIdMismatch()
        {
            var studentCourse = new StudentCourse { StudentId = 1, CourseId = 1, EnrollmentDate = System.DateTime.Now };

            var result = _controller.UpdateStudentCourse(2, 1, studentCourse);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteStudentCourse_ReturnsNoContentResult()
        {
            var result = _controller.DeleteStudentCourse(1, 1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}