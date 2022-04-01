using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.DataLoader;

public class SessionByIdDataLoader : BatchDataLoader<int, Session>
{
    private readonly IDbContextFactory<ConferencePlannerDbContext> contextFactory;

    public SessionByIdDataLoader(IBatchScheduler batchScheduler, IDbContextFactory<ConferencePlannerDbContext> contextFactory)
        : base(batchScheduler)
    {
        this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
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
