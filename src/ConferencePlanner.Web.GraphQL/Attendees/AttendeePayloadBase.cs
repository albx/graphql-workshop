using ConferencePlanner.Data.Models;
using ConferencePlanner.Web.GraphQL.Common;

namespace ConferencePlanner.Web.GraphQL.Attendees;

public class AttendeePayloadBase : Payload
{
    protected AttendeePayloadBase(Attendee attendee)
    {
        Attendee = attendee;
    }

    protected AttendeePayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }

    public Attendee? Attendee { get; }
}
