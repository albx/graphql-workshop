using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.Common;
using ConferencePlanner.Web.GraphQL.Extensions;
using HotChocolate.Subscriptions;

namespace ConferencePlanner.Web.GraphQL.Sessions;

[ExtendObjectType("Mutation")]
public class SessionMutations
{
    [UseConferencePlannerDbContext]
    public async Task<AddSessionPayload> AddSessionAsync(
        AddSessionInput input,
        [ScopedService] ConferencePlannerDbContext context,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(input.Title))
        {
            return new AddSessionPayload(
                new UserError("The title cannot be empty.", "TITLE_EMPTY"));
        }

        if (input.SpeakerIds.Count == 0)
        {
            return new AddSessionPayload(
                new UserError("No speaker assigned.", "NO_SPEAKER"));
        }

        var session = new Session
        {
            Title = input.Title,
            Abstract = input.Abstract,
        };

        foreach (int speakerId in input.SpeakerIds)
        {
            session.SessionSpeakers.Add(new SessionSpeaker
            {
                SpeakerId = speakerId
            });
        }

        context.Sessions.Add(session);
        await context.SaveChangesAsync(cancellationToken);

        return new AddSessionPayload(session);
    }

    [UseConferencePlannerDbContext]
    public async Task<ScheduleSessionPayload> ScheduleSessionAsync(
        ScheduleSessionInput input,
        [ScopedService] ConferencePlannerDbContext context,
        [Service] ITopicEventSender sender)
    {
        if (input.EndTime < input.StartTime)
        {
            return new ScheduleSessionPayload(
                new UserError("endTime has to be larger than startTime.", "END_TIME_INVALID"));
        }

        var session = await context.Sessions.FindAsync(input.SessionId);
        int? initialTrackId = session?.TrackId;

        if (session is null)
        {
            return new ScheduleSessionPayload(new UserError("Session not found.", "SESSION_NOT_FOUND"));
        }

        session.TrackId = input.TrackId;
        session.StartTime = input.StartTime;
        session.EndTime = input.EndTime;

        await context.SaveChangesAsync();

        await sender.SendAsync(nameof(SessionSubscriptions.OnSessionScheduledAsync), session.Id);

        return new ScheduleSessionPayload(session);
    }
}
