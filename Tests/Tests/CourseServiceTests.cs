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
            var course = new Course { Id = 1, Name = "Test Course", Description = "Test Description" };
            _courseService.AddCourse(course);
            Assert.Equal(1, _context.Courses.Count());
        }
    }

}
