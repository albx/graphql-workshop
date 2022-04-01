using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.Extensions;

namespace ConferencePlanner.Web.GraphQL;

public class Mutation
{
    [UseConferencePlannerDbContext]
    public async Task<AddSpeakerPayload> AddSpeakerAsync(
        AddSpeakerInput input,
        [ScopedService] ConferencePlannerDbContext context)
    {
        var speaker = new Speaker
        {
            Bio = input.Bio,
            Name = input.Name,
            WebSite = input.WebSite,
        };

        context.Add(speaker);
        await context.SaveChangesAsync();

        return new AddSpeakerPayload(speaker);
    }
}
