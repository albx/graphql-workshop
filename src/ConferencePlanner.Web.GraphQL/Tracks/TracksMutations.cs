using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.Common;
using ConferencePlanner.Web.GraphQL.Extensions;

namespace ConferencePlanner.Web.GraphQL.Tracks
{
    [ExtendObjectType("Mutation")]
    public class TrackMutations
    {
        [UseConferencePlannerDbContext]
        public async Task<AddTrackPayload> AddTrackAsync(
            AddTrackInput input,
            [ScopedService] ConferencePlannerDbContext context,
            CancellationToken cancellationToken)
        {
            var track = new Track { Name = input.Name };
            context.Tracks.Add(track);

            await context.SaveChangesAsync(cancellationToken);

            return new AddTrackPayload(track);
        }

        [UseConferencePlannerDbContext]
        public async Task<RenameTrackPayload> RenameTrackAsync(
            RenameTrackInput input,
            [ScopedService] ConferencePlannerDbContext context,
            CancellationToken cancellationToken)
        {
            var track = await context.Tracks.FindAsync(input.Id);
            if (track is null)
            {
                return new RenameTrackPayload(new[] { new UserError("Track not found", "TRK001") });
            }

            track.Name = input.Name;

            await context.SaveChangesAsync(cancellationToken);

            return new RenameTrackPayload(track);
        }
    }
}