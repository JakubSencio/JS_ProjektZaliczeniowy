using GraphQL.Types;
using WebAPI.Application.Interfaces;
using WebGraphQLService.Types;

namespace WebGraphQLService.Queries
{
    public AppQuery(ICourseService courseService, IStudentService studentService, IStudentCourseService studentCourseService)
    {
        Field<ListGraphType<CourseType>>(
            "courses",
            resolve: context => courseService.GetAllCourses()
        );

        Field<ListGraphType<StudentType>>(
            "students",
            resolve: context => studentService.GetAllStudents()
        );

        Field<ListGraphType<StudentCourseType>>(
            "studentCourses",
            resolve: context => studentCourseService.GetAllStudentCourses()
        );
    }
}
}