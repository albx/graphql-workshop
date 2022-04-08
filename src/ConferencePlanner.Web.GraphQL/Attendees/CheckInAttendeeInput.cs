using ConferencePlanner.Data.Models;

namespace ConferencePlanner.Web.GraphQL.Attendees;

public record CheckInAttendeeInput(
    [ID(nameof(Session))] int SessionId,
    [ID(nameof(Attendee))] int AttendeeId);
