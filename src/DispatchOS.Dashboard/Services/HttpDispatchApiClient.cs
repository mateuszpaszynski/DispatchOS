using DispatchOS.Contracts.Jobs;
using DispatchOS.Contracts.Planning;
using DispatchOS.Contracts.Technicians;

namespace DispatchOS.Dashboard.Services;

// TODO(#3): implement against DispatchOS.Api using DispatchOS.Contracts.Routes.
public class HttpDispatchApiClient(HttpClient http) : IDispatchApiClient
{
    public Task<JobDto> ReportJobAsync(ReportJobRequest request) => throw new NotImplementedException();
    public Task<JobDto?> GetJobAsync(Guid id) => throw new NotImplementedException();
    public Task<IReadOnlyList<JobDto>> GetJobsAsync(string? status = null) => throw new NotImplementedException();
    public Task UpdateJobStatusAsync(Guid id, string status) => throw new NotImplementedException();
    public Task AssignJobAsync(Guid id, Guid technicianId) => throw new NotImplementedException();
    public Task UnassignJobAsync(Guid id) => throw new NotImplementedException();
    public Task<IReadOnlyList<TechnicianDto>> GetTechniciansAsync() => throw new NotImplementedException();
    public Task<IReadOnlyList<JobDto>> GetTechnicianJobsAsync(Guid technicianId) => throw new NotImplementedException();
    public Task<PlanDto> GeneratePlanAsync(GeneratePlanRequest request) => throw new NotImplementedException();
    public Task<PlanDto?> GetCurrentPlanAsync() => throw new NotImplementedException();
    public Task AcceptCurrentPlanAsync() => throw new NotImplementedException();
}
