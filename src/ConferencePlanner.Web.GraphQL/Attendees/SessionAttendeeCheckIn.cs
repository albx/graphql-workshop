using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Attendees;

public class SessionAttendeeCheckIn
{
    public SessionAttendeeCheckIn(int attendeeId, int sessionId)
    {
        AttendeeId = attendeeId;
        SessionId = sessionId;
    }

    [ID(nameof(Attendee))]
    public int AttendeeId { get; }

    [ID(nameof(Session))]
    public int SessionId { get; }

    [UseConferencePlannerDbContext]
    public async Task<int> CheckInCountAsync(
        [ScopedService] ConferencePlannerDbContext context,
        CancellationToken cancellationToken) =>
        await context.Sessions
            .Where(session => session.Id == SessionId)
            .SelectMany(session => session.SessionAttendees)
            .CountAsync(cancellationToken);

    public Task<Attendee> GetAttendeeAsync(
        AttendeeByIdDataLoader attendeeById,
        CancellationToken cancellationToken) => attendeeById.LoadAsync(AttendeeId, cancellationToken);

    public Task<Session> GetSessionAsync(
        SessionByIdDataLoader sessionById,
        CancellationToken cancellationToken) => sessionById.LoadAsync(AttendeeId, cancellationToken);
}
