using DispatchOS.Contracts;
using DispatchOS.Contracts.Jobs;

namespace DispatchOS.Api.Modules.Jobs;

public static class JobsModule
{
    public static IServiceCollection AddJobsModule(this IServiceCollection services)
    {
        // TODO(#1): register IJobRepository implementation.
        return services;
    }

    public static IEndpointRouteBuilder MapJobsEndpoints(this IEndpointRouteBuilder app)
    {
        // User: report a malfunction. Returns 201 + JobDto.
        app.MapPost(Routes.Api.Jobs, (ReportJobRequest request) => TodoResult());

        // Dispatcher: all submissions (optionally filtered by ?status=).
        app.MapGet(Routes.Api.Jobs, (string? status) => TodoResult());

        // User: status of a submission (incl. planned start = estimated time).
        app.MapGet(Routes.Api.Job, (Guid id) => TodoResult());

        // Technician: Assigned -> InProgress -> Done, or Cancelled.
        app.MapPut(Routes.Api.JobStatus, (Guid id, UpdateJobStatusRequest request) => TodoResult());

        // Dispatcher: manual assignment (respected by the planner) / removing it.
        app.MapPut(Routes.Api.JobAssignment, (Guid id, ManualAssignmentRequest request) => TodoResult());
        app.MapDelete(Routes.Api.JobAssignment, (Guid id) => TodoResult());

        return app;
    }

    private static IResult TodoResult() => Results.StatusCode(StatusCodes.Status501NotImplemented);
}
