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
    public class CoursesControllerTests
    {
        private readonly Mock<ICourseService> _mockCourseService;
        private readonly CoursesController _controller;

        public CoursesControllerTests()
        {
            _mockCourseService = new Mock<ICourseService>();
            _controller = new CoursesController(_mockCourseService.Object);
        }

        [Fact]
        public void GetAllCourses_ReturnsOkResult_WithListOfCourses()
        {
            var courses = new List<Course>
            {
                new Course { Id = 1, Name = "Course 1", Description = "Description 1" },
                new Course { Id = 2, Name = "Course 2", Description = "Description 2" }
            };
            _mockCourseService.Setup(service => service.GetAllCourses()).Returns(courses);

            var result = _controller.GetAllCourses();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCourses = Assert.IsType<List<Course>>(okResult.Value);
            Assert.Equal(2, returnCourses.Count);
        }

        [Fact]
        public void GetCourseById_ReturnsOkResult_WithCourse()
        {
            var course = new Course { Id = 1, Name = "Course 1", Description = "Description 1" };
            _mockCourseService.Setup(service => service.GetCourseById(1)).Returns(course);

            var result = _controller.GetCourseById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCourse = Assert.IsType<Course>(okResult.Value);
            Assert.Equal(1, returnCourse.Id);
        }

        [Fact]
        public void GetCourseById_ReturnsNotFoundResult_WhenCourseNotExists()
        {
            _mockCourseService.Setup(service => service.GetCourseById(1)).Returns((Course)null);

            var result = _controller.GetCourseById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddCourse_ReturnsCreatedAtActionResult_WithCourse()
        {
            var course = new Course { Id = 1, Name = "Course 1", Description = "Description 1" };

            var result = _controller.AddCourse(course);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnCourse = Assert.IsType<Course>(createdAtActionResult.Value);
            Assert.Equal(1, returnCourse.Id);
        }

        [Fact]
        public void UpdateCourse_ReturnsNoContentResult()
        {
            var course = new Course { Id = 1, Name = "Course 1", Description = "Description 1" };

            var result = _controller.UpdateCourse(1, course);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCourse_ReturnsBadRequestResult_WhenIdMismatch()
        {
            var course = new Course { Id = 1, Name = "Course 1", Description = "Description 1" };

            var result = _controller.UpdateCourse(2, course);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteCourse_ReturnsNoContentResult()
        {
            var result = _controller.DeleteCourse(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}