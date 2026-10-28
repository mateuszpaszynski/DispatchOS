using DispatchOS.Api.Modules.Jobs;
using DispatchOS.Api.Modules.Planning;
using DispatchOS.Api.Modules.Technicians;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddJobsModule()
    .AddTechniciansModule()
    .AddPlanningModule(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "DispatchOS.Api");

app.MapJobsEndpoints();
app.MapTechniciansEndpoints();
app.MapPlanningEndpoints();

app.Run();
