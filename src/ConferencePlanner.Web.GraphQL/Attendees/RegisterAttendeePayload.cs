using ConferencePlanner.Data.Models;
using ConferencePlanner.Web.GraphQL.Common;

namespace ConferencePlanner.Web.GraphQL.Attendees;

public class RegisterAttendeePayload : AttendeePayloadBase
{
    public RegisterAttendeePayload(Attendee attendee)
        : base(attendee)
    {
    }

    public RegisterAttendeePayload(UserError error)
        : base(new[] { error })
    {
    }
}
