using GraphQL.Types;
using WebAPI.Domain.Entities;

namespace WebGraphQLService.Types
{
    public class StudentType : ObjectGraphType<Student>
    {
        public StudentType()
        {
            Field(x => x.Id).Description("Student ID");
            Field(x => x.Name).Description("Student Name");
        }
    }
}