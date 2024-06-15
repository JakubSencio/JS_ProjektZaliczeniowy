using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.Services;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Data;

namespace Tests.Tests
{
    public class CourseServiceTests
    {
        private readonly CourseService _courseService;
        private readonly ApplicationDbContext _context;

        public CourseServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _courseService = new CourseService(_context);
        }

        [Fact]
        public void AddCourse_ShouldAddCourse()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var course = new Course { Id = 1, Name = "Test Course", Description = "Test Description" };
            _courseService.AddCourse(course);

            var result = _context.Courses.Find(1);

            Assert.NotNull(result);
            Assert.Equal("Test Course", result.Name);
            Assert.Equal("Test Description", result.Description);
        }

        [Fact]
        public void GetAllCourses_ShouldReturnAllCourses()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _courseService.AddCourse(new Course { Id = 1, Name = "Course 1", Description = "Description 1" });
            _courseService.AddCourse(new Course { Id = 2, Name = "Course 2", Description = "Description 2" });

            var result = _courseService.GetAllCourses();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetCourseById_ShouldReturnCourse()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _courseService.AddCourse(new Course { Id = 1, Name = "Course 1", Description = "Description 1" });

            var result = _courseService.GetCourseById(1);

            Assert.NotNull(result);
            Assert.Equal("Course 1", result.Name);
        }

        [Fact]
        public void UpdateCourse_ShouldUpdateCourse()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var course = new Course { Id = 1, Name = "Course 1", Description = "Description 1" };
            _courseService.AddCourse(course);

            var newEnrollmentDate = DateTime.Now.AddDays(1);
            course.Name = "Updated Course";
            _courseService.UpdateCourse(course);

            var result = _context.Courses.Find(1);

            Assert.NotNull(result);
            Assert.Equal("Updated Course", result.Name);
        }

        [Fact]
        public void DeleteCourse_ShouldRemoveCourse()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var course = new Course { Id = 1, Name = "Course 1", Description = "Description 1" };
            _courseService.AddCourse(course);

            _courseService.DeleteCourse(1);

            var result = _context.Courses.Find(1);

            Assert.Null(result);
        }
    }
}