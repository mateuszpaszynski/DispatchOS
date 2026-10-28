using DispatchOS.Contracts;
using DispatchOS.Contracts.Planning;

namespace DispatchOS.Api.Modules.Planning;

public static class PlanningModule
{
    public static IServiceCollection AddPlanningModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<MatchingEngineClient>(c =>
            c.BaseAddress = new Uri(config["MatchingEngine:BaseUrl"]!));

        // TODO(#1): register plan storage + state version tracking.
        return services;
    }

    public static IEndpointRouteBuilder MapPlanningEndpoints(this IEndpointRouteBuilder app)
    {
        // Dispatcher: build PlanRequest from open jobs + technicians + manual assignments, call the engine, store result.
        app.MapPost(Routes.Api.Plans, (GeneratePlanRequest request) => TodoResult());

        // Dispatcher: last generated plan, with IsStale flag.
        app.MapGet(Routes.Api.CurrentPlan, () => TodoResult());

        // Dispatcher: apply the suggested plan to jobs (Job.AssignTo for every assignment).
        app.MapPost(Routes.Api.AcceptCurrentPlan, () => TodoResult());

        return app;
    }

    private static IResult TodoResult() => Results.StatusCode(StatusCodes.Status501NotImplemented);
}
