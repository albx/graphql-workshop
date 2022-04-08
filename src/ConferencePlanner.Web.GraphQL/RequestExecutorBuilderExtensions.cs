using ConferencePlanner.Web.GraphQL.Attendees;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Sessions;
using ConferencePlanner.Web.GraphQL.Speakers;
using ConferencePlanner.Web.GraphQL.Tracks;
using ConferencePlanner.Web.GraphQL.Types;
using HotChocolate.Execution.Configuration;

namespace ConferencePlanner.Web.GraphQL;

public static class RequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddQueries(this IRequestExecutorBuilder builder)
    {
        builder
            .AddQueryType(q => q.Name("Query"))
            .AddTypeExtension<SpeakerQueries>()
            .AddTypeExtension<SessionQueries>()
            .AddTypeExtension<TrackQueries>()
            .AddTypeExtension<AttendeeQueries>();

        return builder;
    }

    public static IRequestExecutorBuilder AddMutations(this IRequestExecutorBuilder builder)
    {
        builder
            .AddMutationType(m => m.Name("Mutation"))
            .AddTypeExtension<SpeakerMutations>()
            .AddTypeExtension<SessionMutations>()
            .AddTypeExtension<TrackMutations>()
            .AddTypeExtension<AttendeeMutations>();

        return builder;
    }

    public static IRequestExecutorBuilder AddTypes(this IRequestExecutorBuilder builder)
    {
        builder
            .AddType<SpeakerType>()
            .AddType<AttendeeType>()
            .AddType<SessionType>()
            .AddType<TrackType>();

        return builder;
    }

    public static IRequestExecutorBuilder AddDataLoaders(this IRequestExecutorBuilder builder)
    {
        builder
            .AddDataLoader<SpeakerByIdDataLoader>()
            .AddDataLoader<SessionByIdDataLoader>()
            .AddDataLoader<AttendeeByIdDataLoader>()
            .AddDataLoader<TrackByIdDataLoader>();

        return builder;
    }

    public static IRequestExecutorBuilder AddSubscriptions(this IRequestExecutorBuilder builder)
    {
        builder
            .AddSubscriptionType(s => s.Name("Subscription"))
            .AddTypeExtension<SessionSubscriptions>()
            .AddTypeExtension<AttendeeSubscriptions>();

        return builder;
    }
}
