using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Extensions;

namespace ConferencePlanner.Web.GraphQL.Attendees;

[ExtendObjectType("Query")]
public class AttendeeQueries
{
    [UseConferencePlannerDbContext]
    [UsePaging]
    public IQueryable<Attendee> GetAttendees(
        [ScopedService] ConferencePlannerDbContext context) => context.Attendees;

    public Task<Attendee> GetAttendeeByIdAsync(
        [ID(nameof(Attendee))] int id,
        AttendeeByIdDataLoader attendeeById,
        CancellationToken cancellationToken) => attendeeById.LoadAsync(id, cancellationToken);

    public async Task<IEnumerable<Attendee>> GetAttendeesByIdAsync(
        [ID(nameof(Attendee))] int[] ids,
        AttendeeByIdDataLoader attendeeById,
        CancellationToken cancellationToken) => await attendeeById.LoadAsync(ids, cancellationToken);
}
