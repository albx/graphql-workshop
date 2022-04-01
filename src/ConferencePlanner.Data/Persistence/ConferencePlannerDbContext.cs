using ConferencePlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Data.Persistence;

public class ConferencePlannerDbContext : DbContext
{
    public ConferencePlannerDbContext(DbContextOptions<ConferencePlannerDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Attendee>()
            .HasIndex(a => a.UserName)
            .IsUnique();

        // Many-to-many: Session <-> Attendee
        modelBuilder
            .Entity<SessionAttendee>()
            .HasKey(ca => new { ca.SessionId, ca.AttendeeId });

        // Many-to-many: Speaker <-> Session
        modelBuilder
            .Entity<SessionSpeaker>()
            .HasKey(ss => new { ss.SessionId, ss.SpeakerId });
    }

    public DbSet<Session> Sessions { get; set; } = default!;

    public DbSet<Track> Tracks { get; set; } = default!;

    public DbSet<Speaker> Speakers { get; set; } = default!;

    public DbSet<Attendee> Attendees { get; set; } = default!;
}
