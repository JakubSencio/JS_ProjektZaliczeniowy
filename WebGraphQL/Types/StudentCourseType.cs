using GraphQL.Types;
using WebAPI.Domain.Entities;

namespace WebGraphQLService.Types
{
    public class StudentCourseType : ObjectGraphType<StudentCourse>
    {
        public StudentCourseType()
        {
            Field(x => x.StudentId).Description("Student ID");
            Field(x => x.CourseId).Description("Course ID");
        }
    }
}