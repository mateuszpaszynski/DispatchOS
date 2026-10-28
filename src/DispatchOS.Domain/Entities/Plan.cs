using DispatchOS.Domain.Enums;

namespace DispatchOS.Domain.Entities;

public record Assignment(Guid JobId, Guid TechnicianId, int Start, int End, bool IsManuallySet);

public record UnassignedJob(Guid JobId, string Reason);

// Suggested plan for one day. StateVersion identifies the jobs/technicians snapshot it was computed from,
// so a plan becomes stale once anything changes (e.g. a new high-priority job arrives).
public class Plan
{
    public Guid Id { get; private set; }
    public DateOnly Day { get; private set; }
    public PlanningStrategy Strategy { get; private set; }
    public long StateVersion { get; private set; }
    public double Cost { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<Assignment> _assignments = new();
    public IReadOnlyCollection<Assignment> Assignments => _assignments.AsReadOnly();

    private readonly List<UnassignedJob> _unassigned = new();
    public IReadOnlyCollection<UnassignedJob> Unassigned => _unassigned.AsReadOnly();

    public Plan(DateOnly day, PlanningStrategy strategy, long stateVersion, double cost,
        IEnumerable<Assignment> assignments, IEnumerable<UnassignedJob> unassigned)
    {
        Id = Guid.NewGuid();
        Day = day;
        Strategy = strategy;
        StateVersion = stateVersion;
        Cost = cost;
        CreatedAt = DateTime.UtcNow;
        _assignments.AddRange(assignments);
        _unassigned.AddRange(unassigned);
    }

    private Plan() { }

    public bool IsStale(long currentStateVersion) => currentStateVersion != StateVersion;
}
