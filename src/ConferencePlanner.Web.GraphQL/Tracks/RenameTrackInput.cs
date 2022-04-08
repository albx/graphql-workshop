using ConferencePlanner.Data.Models;

namespace ConferencePlanner.Web.GraphQL.Tracks;

public record RenameTrackInput([ID(nameof(Track))] int Id, string Name);
