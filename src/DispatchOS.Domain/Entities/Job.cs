using DispatchOS.Domain.Enums;
using DispatchOS.Domain.ValueObjects;

namespace DispatchOS.Domain.Entities;

public class Job
{
    public Guid Id { get; private set; }
    public string Description { get; private set; }
    public Location Location { get; private set; }
    public TimeWindow Window { get; private set; }
    public JobPriority Priority { get; private set; }
    public JobStatus Status { get; private set; }
    public int EstimatedDurationMinutes { get; private set; }
    public DateTime ReportedAt { get; private set; }

    public Guid? AssignedTechnicianId { get; private set; }
    public bool IsManuallyAssigned { get; private set; }

    private readonly List<Skill> _requiredSkills = new();
    public IReadOnlyCollection<Skill> RequiredSkills => _requiredSkills.AsReadOnly();

    public Job(string description, Location location, TimeWindow window, JobPriority priority,
        int estimatedDurationMinutes, IEnumerable<Skill> requiredSkills)
    {
        if (estimatedDurationMinutes <= 0)
            throw new ArgumentOutOfRangeException(nameof(estimatedDurationMinutes));
        if (estimatedDurationMinutes > window.Duration)
            throw new ArgumentException("Job does not fit in its window.", nameof(window));

        Id = Guid.NewGuid();
        Description = string.IsNullOrWhiteSpace(description) ? throw new ArgumentNullException(nameof(description)) : description;
        Location = location;
        Window = window;
        Priority = priority;
        EstimatedDurationMinutes = estimatedDurationMinutes;
        Status = JobStatus.Reported;
        ReportedAt = DateTime.UtcNow;
        _requiredSkills.AddRange(requiredSkills.Distinct());
    }

    private Job()
    {
        Description = null!;
        Window = null!;
    }

    public void AssignTo(Guid technicianId, bool manually)
    {
        if (Status is not (JobStatus.Reported or JobStatus.Assigned))
            throw new InvalidOperationException($"Cannot assign job in state {Status}.");

        AssignedTechnicianId = technicianId;
        IsManuallyAssigned = manually;
        Status = JobStatus.Assigned;
    }

    public void Unassign()
    {
        if (Status != JobStatus.Assigned)
            throw new InvalidOperationException($"Cannot unassign job in state {Status}.");

        AssignedTechnicianId = null;
        IsManuallyAssigned = false;
        Status = JobStatus.Reported;
    }

    public void Start()
    {
        if (Status != JobStatus.Assigned)
            throw new InvalidOperationException($"Cannot start job in state {Status}.");
        Status = JobStatus.InProgress;
    }

    public void Complete()
    {
        if (Status != JobStatus.InProgress)
            throw new InvalidOperationException($"Cannot complete job in state {Status}.");
        Status = JobStatus.Done;
    }

    public void Cancel()
    {
        if (Status is JobStatus.Done or JobStatus.Cancelled)
            throw new InvalidOperationException($"Cannot cancel job in state {Status}.");
        Status = JobStatus.Cancelled;
    }
}
