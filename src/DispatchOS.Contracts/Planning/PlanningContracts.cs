using DispatchOS.Contracts.Jobs;

namespace DispatchOS.Contracts.Planning;

// --- Dashboard <-> Api ---

public record GeneratePlanRequest(DateOnly Day, string Strategy);

public record PlanDto(
    Guid Id,
    DateOnly Day,
    string Strategy,
    long StateVersion,
    bool IsStale,
    double Cost,
    IReadOnlyList<PlannedAssignment> Assignments,
    IReadOnlyList<PlannedUnassignedJob> Unassigned);

// --- Api <-> MatchingEngine (POST /plan) ---

public record PlanRequest(
    DateOnly Day,
    string Strategy,
    IReadOnlyList<PlanJob> Jobs,
    IReadOnlyList<PlanTechnician> Technicians,
    IReadOnlyList<PlannedAssignment> FixedAssignments);

public record PlanJob(
    Guid Id,
    LocationDto Location,
    int WindowStart,
    int WindowEnd,
    int Priority,
    int DurationMinutes,
    IReadOnlyList<string> RequiredSkills);

public record PlanTechnician(
    Guid Id,
    int ShiftStart,
    int ShiftEnd,
    LocationDto StartLocation,
    IReadOnlyList<string> Skills);

public record PlanResponse(
    double Cost,
    IReadOnlyList<PlannedAssignment> Assignments,
    IReadOnlyList<PlannedUnassignedJob> Unassigned);

public record PlannedAssignment(Guid JobId, Guid TechnicianId, int Start, int End, bool IsManuallySet);

public record PlannedUnassignedJob(Guid JobId, string Reason);
