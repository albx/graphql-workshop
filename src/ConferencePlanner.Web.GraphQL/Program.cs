using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<ConferencePlannerDbContext>(
    options => options.UseSqlite("Data source=conferences.db"));

builder.Services
    .AddGraphQLServer()
    .AddQueries()
    .AddMutations()
    .AddTypes()
    .AddGlobalObjectIdentification()
    .AddDataLoaders();

var app = builder.Build();

app.MapGet("/", () => "Hello GraphQL!");
app.MapGraphQL();

app.Run();
