using ConferencePlanner.Data.Models;
using ConferencePlanner.Web.GraphQL.DataLoader;

namespace ConferencePlanner.Web.GraphQL.Sessions
{
    [ExtendObjectType("Subscription")]
    public class SessionSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Session> OnSessionScheduledAsync(
            [EventMessage] int sessionId,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) => sessionById.LoadAsync(sessionId, cancellationToken);
    }
}