using GraphQL.Types;
using WebAPI.Domain.Entities;

namespace WebGraphQLService.Types
{
    public class CourseType : ObjectGraphType<Course>
    {
        public CourseType()
        {
            Field(x => x.Id).Description("Course ID");
            Field(x => x.Name).Description("Course Name");
        }
    }
}