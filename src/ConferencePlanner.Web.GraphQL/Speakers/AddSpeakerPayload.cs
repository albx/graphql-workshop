using ConferencePlanner.Data.Models;
using ConferencePlanner.Web.GraphQL.Common;

namespace ConferencePlanner.Web.GraphQL.Speakers;

public class AddSpeakerPayload : SpeakerPayloadBase
{
    public AddSpeakerPayload(Speaker speaker)
        : base(speaker)
    {
    }

    public AddSpeakerPayload(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }
}
