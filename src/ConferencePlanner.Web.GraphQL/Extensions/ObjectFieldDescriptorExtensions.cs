using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Extensions;

public static class ObjectFieldDescriptorExtensions
{
    public static IObjectFieldDescriptor UseDbContext<TContext>(this IObjectFieldDescriptor descriptor)
        where TContext : DbContext
    {
        descriptor.UseScopedService<TContext>(
            create: provider => provider.GetRequiredService<IDbContextFactory<TContext>>().CreateDbContext(),
            disposeAsync: (provider, ctx) => ctx.DisposeAsync());

        return descriptor;
    }
}
