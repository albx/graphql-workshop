using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Types;

public class SpeakerType : ObjectType<Speaker>
{
    protected override void Configure(IObjectTypeDescriptor<Speaker> descriptor)
    {
        descriptor
            .Field(t => t.SessionSpeakers)
            .ResolveWith<SpeakerResolver>(t => t.GetSessionsAsync(default!, default!, default!, default!))
            .UseDbContext<ConferencePlannerDbContext>()
            .Name("sessions");
    }

    internal class SpeakerResolver
    {
        public async Task<IEnumerable<Session>> GetSessionsAsync(
            [Parent] Speaker speaker,
            [ScopedService] ConferencePlannerDbContext context,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken)
        {
            var sessionIds = await context.Speakers
                .Where(s => s.Id == speaker.Id)
                .Include(s => s.SessionSpeakers)
                .SelectMany(s => s.SessionSpeakers.Select(x => x.SessionId))
                .ToArrayAsync();

            return await sessionById.LoadAsync(sessionIds, cancellationToken);
        }
    }
}
