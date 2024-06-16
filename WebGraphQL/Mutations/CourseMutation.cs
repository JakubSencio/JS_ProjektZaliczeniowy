using GraphQL;
using GraphQL.Types;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;
using WebGraphQLService.Types;

namespace WebGraphQLService.Mutations
{
    public class CourseMutation : ObjectGraphType
    {
        public CourseMutation(ICourseService courseService)
        {
            Field<CourseType>(
                "createCourse",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    var course = new Course { Name = name };
                    return courseService.CreateCourse(course);
                });

            Field<CourseType>(
                "updateCourse",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var name = context.GetArgument<string>("name");
                    var course = new Course { Id = id, Name = name };
                    return courseService.UpdateCourse(course);
                });

            Field<BooleanGraphType>(
                "deleteCourse",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    courseService.DeleteCourse(id);
                    return true;
                });
        }
    }
}