using ConferencePlanner.Data.Persistence;
using ConferencePlanner.Web.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ConferencePlannerDbContext>(
    options => options.UseSqlite("Data source=conferences.db"));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();
