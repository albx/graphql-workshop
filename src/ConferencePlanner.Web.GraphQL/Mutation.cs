using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;

namespace ConferencePlanner.Web.GraphQL;

public class Mutation
{
    public async Task<AddSpeakerPayload> AddSpeakerAsync(
        AddSpeakerInput input,
        [Service] ConferencePlannerDbContext context)
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
