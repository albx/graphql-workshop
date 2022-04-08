﻿using ConferencePlanner.Data.Models;
using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL.DataLoader;
using ConferencePlanner.Web.GraphQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Web.GraphQL.Types;

public class TrackType : ObjectType<Track>
{
    protected override void Configure(IObjectTypeDescriptor<Track> descriptor)
    {
        descriptor
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) =>
                ctx.DataLoader<TrackByIdDataLoader>().LoadAsync(id, ctx.RequestAborted)!);

        descriptor
            .Field(t => t.Sessions)
            .ResolveWith<TrackResolvers>(t => t.GetSessionsAsync(default!, default!, default!, default))
            .UseDbContext<ConferencePlannerDbContext>()
            .Name("sessions");
    }

    internal class TrackResolvers
    {
        public async Task<IEnumerable<Session>> GetSessionsAsync(
            Track track,
            [ScopedService] ConferencePlannerDbContext context,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken)
        {
            int[] sessionIds = await context.Sessions
                .Where(s => s.Id == track.Id)
                .Select(s => s.Id)
                .ToArrayAsync();

            return await sessionById.LoadAsync(sessionIds, cancellationToken);
        }
    }
}