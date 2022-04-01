using ConferencePlanner.Data.Persistence;
using HotChocolate.Types.Descriptors;
using System.Reflection;

namespace ConferencePlanner.Web.GraphQL.Extensions;

public sealed class UseConferencePlannerDbContextAttribute : ObjectFieldDescriptorAttribute
{
    public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
    {
        descriptor.UseDbContext<ConferencePlannerDbContext>();
    }
}
