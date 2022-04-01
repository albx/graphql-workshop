using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;

namespace ConferencePlanner.Web.GraphQL;

public class Query
{
    public IQueryable<Speaker> GetSpeakers([Service] ConferencePlannerDbContext context) => context.Speakers;
}
