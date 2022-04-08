using ConferencePlanner.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Common
{
    public abstract class BaseBatchDataLoader<TKey, TValue> : BatchDataLoader<TKey, TValue>
        where TKey : notnull
        where TValue : class
    {
        protected readonly IDbContextFactory<ConferencePlannerDbContext> contextFactory;

        protected BaseBatchDataLoader(
            IDbContextFactory<ConferencePlannerDbContext> contextFactory,
            IBatchScheduler batchScheduler) : base(batchScheduler)
        {
            this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }
    }
}
