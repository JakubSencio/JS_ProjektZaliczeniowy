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
    public class StudentsControllerTests
    {
        private readonly Mock<IStudentService> _mockStudentService;
        private readonly StudentsController _controller;

        public StudentsControllerTests()
        {
            _mockStudentService = new Mock<IStudentService>();
            _controller = new StudentsController(_mockStudentService.Object);
        }

        [Fact]
        public void GetAllStudents_ReturnsOkResult_WithListOfStudents()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Student 1", Email = "student1@example.com" },
                new Student { Id = 2, Name = "Student 2", Email = "student2@example.com" }
            };
            _mockStudentService.Setup(service => service.GetAllStudents()).Returns(students);

            var result = _controller.GetAllStudents();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnStudents = Assert.IsType<List<Student>>(okResult.Value);
            Assert.Equal(2, returnStudents.Count);
        }

        [Fact]
        public void GetStudentById_ReturnsOkResult_WithStudent()
        {
            var student = new Student { Id = 1, Name = "Student 1", Email = "student1@example.com" };
            _mockStudentService.Setup(service => service.GetStudentById(1)).Returns(student);

            var result = _controller.GetStudentById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnStudent = Assert.IsType<Student>(okResult.Value);
            Assert.Equal(1, returnStudent.Id);
        }

        [Fact]
        public void GetStudentById_ReturnsNotFoundResult_WhenStudentNotExists()
        {
            _mockStudentService.Setup(service => service.GetStudentById(1)).Returns((Student)null);

            var result = _controller.GetStudentById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AddStudent_ReturnsCreatedAtActionResult_WithStudent()
        {
            var student = new Student { Id = 1, Name = "Student 1", Email = "student1@example.com" };

            var result = _controller.AddStudent(student);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnStudent = Assert.IsType<Student>(createdAtActionResult.Value);
            Assert.Equal(1, returnStudent.Id);
        }

        [Fact]
        public void UpdateStudent_ReturnsNoContentResult()
        {
            var student = new Student { Id = 1, Name = "Student 1", Email = "student1@example.com" };

            var result = _controller.UpdateStudent(1, student);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateStudent_ReturnsBadRequestResult_WhenIdMismatch()
        {
            var student = new Student { Id = 1, Name = "Student 1", Email = "student1@example.com" };

            var result = _controller.UpdateStudent(2, student);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteStudent_ReturnsNoContentResult()
        {
            var result = _controller.DeleteStudent(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}