using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<ConferencePlannerDbContext>(
    options => options.UseSqlite("Data source=conferences.db"));

builder.Services
    .AddGraphQLServer()
    .AddGlobalObjectIdentification()
    .AddQueries()
    .AddMutations()
    .AddSubscriptions()
    .AddTypes()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions()
    .AddDataLoaders();

var app = builder.Build();

app.UseWebSockets();
app.UseRouting();

app.MapGet("/", () => "Hello GraphQL!");
app.MapGraphQL();

app.Run();
