using DispatchOS.Contracts;
using DispatchOS.Contracts.Technicians;

namespace DispatchOS.Api.Modules.Technicians;

public static class TechniciansModule
{
    public static IServiceCollection AddTechniciansModule(this IServiceCollection services)
    {
        // TODO(#1): register ITechnicianRepository implementation + seed data.
        return services;
    }

    public static IEndpointRouteBuilder MapTechniciansEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Api.Technicians, () => TodoResult());
        app.MapGet(Routes.Api.Technician, (Guid id) => TodoResult());
        app.MapPost(Routes.Api.Technicians, (CreateTechnicianRequest request) => TodoResult());

        // Technician: jobs assigned to me today, ordered by planned start.
        app.MapGet(Routes.Api.TechnicianJobs, (Guid id) => TodoResult());

        return app;
    }

    private static IResult TodoResult() => Results.StatusCode(StatusCodes.Status501NotImplemented);
}
