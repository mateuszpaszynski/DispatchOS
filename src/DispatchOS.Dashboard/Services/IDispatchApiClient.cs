using DispatchOS.Contracts.Jobs;
using DispatchOS.Contracts.Planning;
using DispatchOS.Contracts.Technicians;

namespace DispatchOS.Dashboard.Services;

public interface IDispatchApiClient
{
    Task<JobDto> ReportJobAsync(ReportJobRequest request);
    Task<JobDto?> GetJobAsync(Guid id);
    Task<IReadOnlyList<JobDto>> GetJobsAsync(string? status = null);
    Task UpdateJobStatusAsync(Guid id, string status);
    Task AssignJobAsync(Guid id, Guid technicianId);
    Task UnassignJobAsync(Guid id);

    Task<IReadOnlyList<TechnicianDto>> GetTechniciansAsync();
    Task<IReadOnlyList<JobDto>> GetTechnicianJobsAsync(Guid technicianId);

    Task<PlanDto> GeneratePlanAsync(GeneratePlanRequest request);
    Task<PlanDto?> GetCurrentPlanAsync();
    Task AcceptCurrentPlanAsync();
}
