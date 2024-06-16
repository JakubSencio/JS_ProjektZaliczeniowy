using GraphQL.Types;
using WebAPI.Application.Interfaces;
using WebGraphQLService.Types;

namespace WebGraphQLService.Mutations
{
    public AppMutation(ICourseService courseService, IStudentService studentService, IStudentCourseService studentCourseService)
    {
        Field<CourseType>(
            "createCourse",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
            ),
            resolve: context =>
            {
                var name = context.GetArgument<string>("name");
                return courseService.CreateCourse(new Domain.Entities.Course { Name = name });
            });

    }
}