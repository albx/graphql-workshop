using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.Common;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Sessions;

public class ScheduleSessionPayload : SessionPayloadBase
{
    public ScheduleSessionPayload(Session session)
        : base(session)
    {
    }

    public ScheduleSessionPayload(UserError error)
        : base(new[] { error })
    {
    }

    public async Task<Track?> GetTrackAsync(
        TrackByIdDataLoader trackById,
        CancellationToken cancellationToken)
    {
        if (Session is null)
        {
            return null;
        }

        return await trackById.LoadAsync(Session.Id, cancellationToken);
    }

    [UseConferencePlannerDbContext]
    public async Task<IEnumerable<Speaker>?> GetSpeakersAsync(
        [ScopedService] ConferencePlannerDbContext dbContext,
        SpeakerByIdDataLoader speakerById,
        CancellationToken cancellationToken)
    {
        if (Session is null)
        {
            return null;
        }

        int[] speakerIds = await dbContext.Sessions
            .Where(s => s.Id == Session.Id)
            .Include(s => s.SessionSpeakers)
            .SelectMany(s => s.SessionSpeakers.Select(t => t.SpeakerId))
            .ToArrayAsync();

        return await speakerById.LoadAsync(speakerIds, cancellationToken);
    }
}
