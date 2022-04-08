using ConferencePlanner.Data.Models;
using ConferencePlanner.Web.GraphQL.Common;

namespace ConferencePlanner.Web.GraphQL.Sessions;

public class AddSessionPayload : SessionPayloadBase
{
    public AddSessionPayload(UserError error) : base(new[] { error }) { }

    public AddSessionPayload(Session session)
        : base(session)
    {
    }

    public AddSessionPayload(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }
}
