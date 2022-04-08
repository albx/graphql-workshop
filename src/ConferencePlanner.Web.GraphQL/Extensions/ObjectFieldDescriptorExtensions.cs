using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Extensions;

public static class ObjectFieldDescriptorExtensions
{
    public static IObjectFieldDescriptor UseDbContext<TContext>(this IObjectFieldDescriptor descriptor)
        where TContext : DbContext
    {
        descriptor.UseScopedService(
            create: provider => provider.GetRequiredService<IDbContextFactory<TContext>>().CreateDbContext()
            /*disposeAsync: (provider, ctx) => ctx.DisposeAsync()*/);

        return descriptor;
    }

    public static IObjectFieldDescriptor UseUpperCase(this IObjectFieldDescriptor descriptor)
    {
        return descriptor.Use(next => async ctx =>
        {
            await next(ctx);

            if (ctx.Result is string s)
            {
                ctx.Result = s.ToUpperInvariant();
            }
        });
    }
}
