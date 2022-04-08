using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Extensions;
using ConferencePlanner.Web.GraphQL.Types;

namespace ConferencePlanner.Web.GraphQL.Sessions;

[ExtendObjectType("Query")]
public class SessionQueries
{
    [UseConferencePlannerDbContext]
    [UsePaging(typeof(NonNullType<SessionType>))]
    [UseFiltering(typeof(SessionFilterInputType))]
    [UseSorting]
    public IQueryable<Session> GetSessions(
        [ScopedService] ConferencePlannerDbContext context) => context.Sessions;

    public Task<Session> GetSessionByIdAsync(
        [ID(nameof(Session))] int id,
        SessionByIdDataLoader sessionById,
        CancellationToken cancellationToken) => sessionById.LoadAsync(id, cancellationToken);

    public async Task<IEnumerable<Session>> GetSessionsByIdAsync(
        [ID(nameof(Session))] int[] ids,
        SessionByIdDataLoader sessionById,
        CancellationToken cancellationToken) => await sessionById.LoadAsync(ids, cancellationToken);
}
