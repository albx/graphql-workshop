using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.Common;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.DataLoader;

public class TrackByIdDataLoader : BaseBatchDataLoader<int, Track>
{
    public TrackByIdDataLoader(IDbContextFactory<ConferencePlannerDbContext> contextFactory, IBatchScheduler batchScheduler)
        : base(contextFactory, batchScheduler)
    {
    }

    protected override async Task<IReadOnlyDictionary<int, Track>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
        await using var context = contextFactory.CreateDbContext();

        return await context.Tracks
            .Where(t => keys.Contains(t.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);
    }
}
