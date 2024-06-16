using System.Web.Http.Dependencies;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebGraphQLService.Schema
{
    public class AppSchema : Schema
    {
        public AppSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<AppQuery>();
            Mutation = resolver.Resolve<AppMutation>();
        }
    }
}