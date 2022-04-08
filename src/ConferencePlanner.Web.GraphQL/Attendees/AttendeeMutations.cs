using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.Common;
using ConferencePlanner.Web.GraphQL.Extensions;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Attendees;

[ExtendObjectType("Mutation")]
public class AttendeeMutations
{
    [UseConferencePlannerDbContext]
    public async Task<RegisterAttendeePayload> RegisterAttendeeAsync(
        RegisterAttendeeInput input,
        [ScopedService] ConferencePlannerDbContext context,
        CancellationToken cancellationToken)
    {
        var attendee = new Attendee
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            UserName = input.UserName,
            EmailAddress = input.EmailAddress
        };

        context.Attendees.Add(attendee);

        await context.SaveChangesAsync(cancellationToken);

        return new RegisterAttendeePayload(attendee);
    }

    [UseConferencePlannerDbContext]
    public async Task<CheckInAttendeePayload> CheckInAttendeeAsync(
        CheckInAttendeeInput input,
        [ScopedService] ConferencePlannerDbContext context,
        [Service] ITopicEventSender sender,
        CancellationToken cancellationToken)
    {
        var attendee = await context.Attendees
            .FirstOrDefaultAsync(t => t.Id == input.AttendeeId, cancellationToken);

        if (attendee is null)
        {
            return new CheckInAttendeePayload(
                new UserError("Attendee not found.", "ATTENDEE_NOT_FOUND"));
        }

        attendee.SessionsAttendees.Add(
            new SessionAttendee
            {
                SessionId = input.SessionId
            });

        await context.SaveChangesAsync(cancellationToken);

        await sender.SendAsync($"OnAttendeeCheckedIn_{input.SessionId}", input.AttendeeId, cancellationToken);

        return new CheckInAttendeePayload(attendee, input.SessionId);
    }
}
