namespace ConferencePlanner.Web.GraphQL.Attendees;

public record RegisterAttendeeInput(
    string FirstName,
    string LastName,
    string UserName,
    string EmailAddress);
