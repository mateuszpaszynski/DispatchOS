using DispatchOS.Contracts.Jobs;
using DispatchOS.Contracts.Planning;
using DispatchOS.Contracts.Technicians;

namespace DispatchOS.Dashboard.Services;

// In-memory stand-in for DispatchOS.Api, so the UI can be built before the backend is ready.
// Enabled with "UseFakeApi": true in appsettings.
public class FakeDispatchApiClient : IDispatchApiClient
{
    private readonly List<TechnicianDto> _technicians =
    [
        new(Guid.NewGuid(), "Anna", 480, 960, new LocationDto(0, 0), ["plumbing", "electric"]),
        new(Guid.NewGuid(), "Piotr", 600, 1080, new LocationDto(10, 5), ["hvac"])
    ];

    private readonly List<JobDto> _jobs =
    [
        new(Guid.NewGuid(), "Leaking pipe in bathroom", new LocationDto(3, 4), 480, 720, "High", "Reported", 60, ["plumbing"], null, false, null, null),
        new(Guid.NewGuid(), "AC not cooling", new LocationDto(12, 7), 600, 900, "Normal", "Reported", 90, ["hvac"], null, false, null, null)
    ];

    private PlanDto? _plan;

    public Task<JobDto> ReportJobAsync(ReportJobRequest r)
    {
        var job = new JobDto(Guid.NewGuid(), r.Description, r.Location, r.WindowStart, r.WindowEnd, r.Priority,
            "Reported", r.EstimatedDurationMinutes, r.RequiredSkills, null, false, null, null);
        _jobs.Add(job);
        return Task.FromResult(job);
    }

    public Task<JobDto?> GetJobAsync(Guid id) => Task.FromResult(_jobs.FirstOrDefault(j => j.Id == id));

    public Task<IReadOnlyList<JobDto>> GetJobsAsync(string? status = null) =>
        Task.FromResult<IReadOnlyList<JobDto>>(_jobs.Where(j => status is null || j.Status == status).ToList());

    public Task UpdateJobStatusAsync(Guid id, string status) => Replace(id, j => j with { Status = status });

    public Task AssignJobAsync(Guid id, Guid technicianId) =>
        Replace(id, j => j with { AssignedTechnicianId = technicianId, IsManuallyAssigned = true, Status = "Assigned" });

    public Task UnassignJobAsync(Guid id) =>
        Replace(id, j => j with { AssignedTechnicianId = null, IsManuallyAssigned = false, Status = "Reported" });

    public Task<IReadOnlyList<TechnicianDto>> GetTechniciansAsync() =>
        Task.FromResult<IReadOnlyList<TechnicianDto>>(_technicians);

    public Task<IReadOnlyList<JobDto>> GetTechnicianJobsAsync(Guid technicianId) =>
        Task.FromResult<IReadOnlyList<JobDto>>(_jobs.Where(j => j.AssignedTechnicianId == technicianId).ToList());

    public Task<PlanDto> GeneratePlanAsync(GeneratePlanRequest request)
    {
        // Naive fake: first technician gets every open job back-to-back from the job's window start.
        var tech = _technicians[0];
        var assignments = _jobs
            .Where(j => j.Status == "Reported")
            .Select(j => new PlannedAssignment(j.Id, tech.Id, j.WindowStart, j.WindowStart + j.EstimatedDurationMinutes, false))
            .ToList();
        _plan = new PlanDto(Guid.NewGuid(), request.Day, request.Strategy, 1, false, 0, assignments, []);
        return Task.FromResult(_plan);
    }

    public Task<PlanDto?> GetCurrentPlanAsync() => Task.FromResult(_plan);

    public async Task AcceptCurrentPlanAsync()
    {
        foreach (var a in _plan?.Assignments ?? [])
            await Replace(a.JobId, j => j with { AssignedTechnicianId = a.TechnicianId, Status = "Assigned", PlannedStart = a.Start, PlannedEnd = a.End });
    }

    private Task Replace(Guid id, Func<JobDto, JobDto> update)
    {
        var i = _jobs.FindIndex(j => j.Id == id);
        if (i >= 0) _jobs[i] = update(_jobs[i]);
        return Task.CompletedTask;
    }
}
