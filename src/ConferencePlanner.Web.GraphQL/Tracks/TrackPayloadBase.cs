using ConferencePlanner.Data.Models;
using ConferencePlanner.Web.GraphQL.Common;

namespace ConferencePlanner.Web.GraphQL.Tracks;

public class TrackPayloadBase : Payload
{
    public TrackPayloadBase(Track track)
    {
        Track = track;
    }

    public TrackPayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }

    public Track? Track { get; }
}
