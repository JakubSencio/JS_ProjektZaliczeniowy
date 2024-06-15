using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;

namespace WebAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentCoursesController : ControllerBase
    {
        private readonly IStudentCourseService _studentCourseService;

        public StudentCoursesController(IStudentCourseService studentCourseService)
        {
            _studentCourseService = studentCourseService;
        }

        [HttpGet]
        public IActionResult GetAllStudentCourses() => Ok(_studentCourseService.GetAllStudentCourses());

        [HttpGet("{studentId}/{courseId}")]
        public IActionResult GetStudentCourseById(int studentId, int courseId)
        {
            var studentCourse = _studentCourseService.GetStudentCourseById(studentId, courseId);
            if (studentCourse == null)
            {
                return NotFound();
            }
            return Ok(studentCourse);
        }

        [HttpPost]
        public IActionResult AddStudentCourse(StudentCourse studentCourse)
        {
            _studentCourseService.AddStudentCourse(studentCourse);
            return CreatedAtAction(nameof(GetStudentCourseById), new { studentId = studentCourse.StudentId, courseId = studentCourse.CourseId }, studentCourse);
        }

        [HttpPut("{studentId}/{courseId}")]
        public IActionResult UpdateStudentCourse(int studentId, int courseId, StudentCourse studentCourse)
        {
            if (studentId != studentCourse.StudentId || courseId != studentCourse.CourseId)
            {
                return BadRequest();
            }
            _studentCourseService.UpdateStudentCourse(studentCourse);
            return NoContent();
        }

        [HttpDelete("{studentId}/{courseId}")]
        public IActionResult DeleteStudentCourse(int studentId, int courseId)
        {
            _studentCourseService.DeleteStudentCourse(studentId, courseId);
            return NoContent();
        }
    }
}
