using ConferencePlanner.Data.Models;
using ConferencePlanner.Web.GraphQL.Common;

namespace ConferencePlanner.Web.GraphQL.Speakers;

public abstract class SpeakerPayloadBase : Payload
{
    protected SpeakerPayloadBase(Speaker speaker) => Speaker = speaker;

    protected SpeakerPayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }

    public Speaker? Speaker { get; }
}
