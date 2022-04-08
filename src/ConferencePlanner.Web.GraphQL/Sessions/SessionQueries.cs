using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Sessions;

[ExtendObjectType("Query")]
public class SessionQueries
{
    [UseConferencePlannerDbContext]
    public async Task<IEnumerable<Session>> GetSessionsAsync(
        [ScopedService] ConferencePlannerDbContext context,
        CancellationToken cancellationToken) =>
        await context.Sessions.ToListAsync(cancellationToken);

    public Task<Session> GetSessionByIdAsync(
        [ID(nameof(Session))] int id,
        SessionByIdDataLoader sessionById,
        CancellationToken cancellationToken) => sessionById.LoadAsync(id, cancellationToken);

    public async Task<IEnumerable<Session>> GetSessionsByIdAsync(
        [ID(nameof(Session))] int[] ids,
        SessionByIdDataLoader sessionById,
        CancellationToken cancellationToken) => await sessionById.LoadAsync(ids, cancellationToken);
}
