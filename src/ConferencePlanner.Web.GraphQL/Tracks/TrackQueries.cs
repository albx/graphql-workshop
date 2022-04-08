using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Tracks;

[ExtendObjectType("Query")]
public class TrackQueries
{
    [UseConferencePlannerDbContext]
    public async Task<IEnumerable<Track>> GetTracksAsync(
        [ScopedService] ConferencePlannerDbContext context,
        CancellationToken cancellationToken) => await context.Tracks.ToListAsync(cancellationToken);

    [UseConferencePlannerDbContext]
    public Task<Track> GetTrackByNameAsync(
        string name,
        [ScopedService] ConferencePlannerDbContext context,
        CancellationToken cancellationToken) => context.Tracks.FirstAsync(t => t.Name == name);

    [UseConferencePlannerDbContext]
    public async Task<IEnumerable<Track>> GetTrackByNamesAsync(
        string[] names,
        [ScopedService] ConferencePlannerDbContext context,
        CancellationToken cancellationToken) => await context.Tracks.Where(t => names.Contains(t.Name)).ToListAsync();

    public Task<Track> GetTrackByIdAsync(
        [ID(nameof(Track))] int id,
        TrackByIdDataLoader trackById,
        CancellationToken cancellationToken) =>
        trackById.LoadAsync(id, cancellationToken);

    public async Task<IEnumerable<Track>> GetTracksByIdAsync(
        [ID(nameof(Track))] int[] ids,
        TrackByIdDataLoader trackById,
        CancellationToken cancellationToken) => await trackById.LoadAsync(ids, cancellationToken);
}
