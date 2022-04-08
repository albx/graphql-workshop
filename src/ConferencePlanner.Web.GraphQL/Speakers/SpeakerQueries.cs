using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Speakers;

[ExtendObjectType("Query")]
public class SpeakerQueries
{
    [UseConferencePlannerDbContext]
    public Task<List<Speaker>> GetSpeakers([ScopedService] ConferencePlannerDbContext context) => context.Speakers.ToListAsync();

    public Task<Speaker> GetSpeakerByIdAsync(
        [ID(nameof(Speaker))] int id,
        SpeakerByIdDataLoader dataLoader,
        CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);

    public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
        [ID(nameof(Speaker))] int[] ids,
        SpeakerByIdDataLoader dataLoader,
        CancellationToken cancellationToken) => await dataLoader.LoadAsync(ids, cancellationToken);
}
