using ConferencePlanner.Data.Models;
using ConferencePlanner.Web.GraphQL.Common;
using ConferencePlanner.Web.GraphQL.DataLoader;

namespace ConferencePlanner.Web.GraphQL.Attendees;

public class CheckInAttendeePayload : AttendeePayloadBase
{
    private int? _sessionId;

    public CheckInAttendeePayload(Attendee attendee, int sessionId)
        : base(attendee)
    {
        _sessionId = sessionId;
    }

    public CheckInAttendeePayload(UserError error)
        : base(new[] { error })
    {
    }

    public async Task<Session?> GetSessionAsync(
        SessionByIdDataLoader sessionById,
        CancellationToken cancellationToken)
    {
        if (_sessionId.HasValue)
        {
            return await sessionById.LoadAsync(_sessionId.Value, cancellationToken);
        }

        return null;
    }
}
