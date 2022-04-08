using ConferencePlanner.Data.Models;
using HotChocolate.Data.Filters;

namespace ConferencePlanner.Web.GraphQL.Sessions;

public class SessionFilterInputType : FilterInputType<Session>
{
    protected override void Configure(IFilterInputTypeDescriptor<Session> descriptor)
    {
        descriptor.Ignore(s => s.Id);
        descriptor.Ignore(s => s.TrackId);
    }
}
