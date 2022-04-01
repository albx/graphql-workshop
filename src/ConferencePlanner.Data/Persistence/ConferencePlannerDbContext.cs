using ConferencePlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Data.Persistence;

public class ConferencePlannerDbContext : DbContext
{
    public ConferencePlannerDbContext(DbContextOptions<ConferencePlannerDbContext> options)
        : base(options)
    {

    }

    public DbSet<Speaker> Speakers { get; set; }
}
