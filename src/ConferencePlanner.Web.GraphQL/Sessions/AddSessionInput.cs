using ConferencePlanner.Data.Models;

namespace ConferencePlanner.Web.GraphQL.Sessions;

public record AddSessionInput(
    string Title,
    string? Abstract,
    [ID(nameof(Speaker))] IReadOnlyList<int> SpeakerIds);
