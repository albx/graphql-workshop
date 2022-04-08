using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.Common;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.DataLoader;

public class AttendeeByIdDataLoader : BaseBatchDataLoader<int, Attendee>
{
    public AttendeeByIdDataLoader(IDbContextFactory<ConferencePlannerDbContext> contextFactory, IBatchScheduler batchScheduler)
        : base(contextFactory, batchScheduler)
    {
    }

    protected override async Task<IReadOnlyDictionary<int, Attendee>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
        await using var context = contextFactory.CreateDbContext();

        return await context.Attendees
            .Where(a => keys.Contains(a.Id))
            .ToDictionaryAsync(a => a.Id, cancellationToken);
    }
}
