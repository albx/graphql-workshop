using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.Common;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.DataLoader;

public class SessionByIdDataLoader : BaseBatchDataLoader<int, Session>
{
    public SessionByIdDataLoader(IDbContextFactory<ConferencePlannerDbContext> contextFactory, IBatchScheduler batchScheduler)
        : base(contextFactory, batchScheduler)
    {
    }

    protected override async Task<IReadOnlyDictionary<int, Session>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
        await using var context = contextFactory.CreateDbContext();

        var sessions = await context.Sessions
            .Where(s => keys.Contains(s.Id))
            .ToDictionaryAsync(s => s.Id, cancellationToken);

        return sessions;
    }
}
