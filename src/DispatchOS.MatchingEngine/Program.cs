using DispatchOS.Contracts;
using DispatchOS.Contracts.Planning;
using DispatchOS.MatchingEngine.Planning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPlanner, Planner>();

var app = builder.Build();

app.MapGet("/", () => "DispatchOS.MatchingEngine");

app.MapPost(Routes.MatchingEngine.Plan, (PlanRequest request, IPlanner planner) =>
    Results.Ok(planner.CreatePlan(request)));

app.Run();
