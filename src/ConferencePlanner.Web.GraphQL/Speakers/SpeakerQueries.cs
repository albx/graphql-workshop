using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Extensions;

namespace ConferencePlanner.Web.GraphQL.Speakers;

[ExtendObjectType("Query")]
public class SpeakerQueries
{
    [UseConferencePlannerDbContext]
    [UsePaging]
    public IQueryable<Speaker> GetSpeakers(
        [ScopedService] ConferencePlannerDbContext context) => context.Speakers.OrderBy(s => s.Name);

    public Task<Speaker> GetSpeakerByIdAsync(
        [ID(nameof(Speaker))] int id,
        SpeakerByIdDataLoader dataLoader,
        CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);

    public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
        [ID(nameof(Speaker))] int[] ids,
        SpeakerByIdDataLoader dataLoader,
        CancellationToken cancellationToken) => await dataLoader.LoadAsync(ids, cancellationToken);
}
