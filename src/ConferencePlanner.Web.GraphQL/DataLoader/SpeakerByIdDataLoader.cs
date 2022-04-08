using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.Common;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.DataLoader;

public class SpeakerByIdDataLoader : BaseBatchDataLoader<int, Speaker>
{
    public SpeakerByIdDataLoader(IDbContextFactory<ConferencePlannerDbContext> contextFactory, IBatchScheduler scheduler)
        : base(contextFactory, scheduler)
    {
    }

    protected override async Task<IReadOnlyDictionary<int, Speaker>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
        await using var context = contextFactory.CreateDbContext();

        var speakers = await context.Speakers
            .Where(s => keys.Contains(s.Id))
            .ToDictionaryAsync(s => s.Id, cancellationToken);

        return speakers;
    }
}
