using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.DataLoader;

public class SpeakerByIdDataLoader : BatchDataLoader<int, Speaker>
{
    private readonly IDbContextFactory<ConferencePlannerDbContext> contextFactory;

    public SpeakerByIdDataLoader(IBatchScheduler scheduler, IDbContextFactory<ConferencePlannerDbContext> contextFactory)
        : base(scheduler)
    {
        this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
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
