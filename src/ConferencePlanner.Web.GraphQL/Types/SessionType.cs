using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Types;

public class SessionType : ObjectType<Session>
{
    protected override void Configure(IObjectTypeDescriptor<Session> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) => ctx.DataLoader<SessionByIdDataLoader>().LoadAsync(id, ctx.RequestAborted)!);

        descriptor
            .Field(t => t.SessionSpeakers)
            .ResolveWith<SessionResolvers>(t => t.GetSpeakersAsync(default!, default!, default!, default))
            .UseDbContext<ConferencePlannerDbContext>()
            .Name("speakers");

        descriptor
            .Field(t => t.SessionAttendees)
            .ResolveWith<SessionResolvers>(t => t.GetAttendeesAsync(default!, default!, default!, default))
            .UseDbContext<ConferencePlannerDbContext>()
            .Name("attendees");

        descriptor
            .Field(t => t.Track)
            .ResolveWith<SessionResolvers>(t => t.GetTrackAsync(default!, default!, default));

        descriptor
            .Field(t => t.TrackId)
            .ID(nameof(Track));
    }

    internal class SessionResolvers
    {
        public async Task<IEnumerable<Speaker>> GetSpeakersAsync(
            [Parent] Session session,
            [ScopedService] ConferencePlannerDbContext context,
            SpeakerByIdDataLoader speakerById,
            CancellationToken cancellationToken)
        {
            int[] speakerIds = await context.Sessions
                .Where(s => s.Id == session.Id)
                .Include(s => s.SessionSpeakers)
                .SelectMany(s => s.SessionSpeakers.Select(t => t.SpeakerId))
                .ToArrayAsync();

            return await speakerById.LoadAsync(speakerIds, cancellationToken);
        }

        public async Task<IEnumerable<Attendee>> GetAttendeesAsync(
            Session session,
            [ScopedService] ConferencePlannerDbContext dbContext,
            AttendeeByIdDataLoader attendeeById,
            CancellationToken cancellationToken)
        {
            int[] attendeeIds = await dbContext.Sessions
                .Where(s => s.Id == session.Id)
                .Include(session => session.SessionAttendees)
                .SelectMany(session => session.SessionAttendees.Select(t => t.AttendeeId))
                .ToArrayAsync();

            return await attendeeById.LoadAsync(attendeeIds, cancellationToken);
        }

        public async Task<Track?> GetTrackAsync(
            Session session,
            TrackByIdDataLoader trackById,
            CancellationToken cancellationToken)
        {
            if (session.TrackId is null)
            {
                return null;
            }

            return await trackById.LoadAsync(session.TrackId.Value, cancellationToken);
        }
    }
}
