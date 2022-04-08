using ConferencePlanner.Data.Models;
using ConferencePlanner.Web.GraphQL.Common;

namespace ConferencePlanner.Web.GraphQL.Tracks;

public class RenameTrackPayload : TrackPayloadBase
{
    public RenameTrackPayload(Track track)
        : base(track)
    {
    }

    public RenameTrackPayload(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }
}
