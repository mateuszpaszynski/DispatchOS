namespace DispatchOS.Contracts.Jobs;

// Times are minutes from midnight of the planned day. Status/Priority are enum names from DispatchOS.Domain.Enums.

public record LocationDto(int X, int Y);

public record ReportJobRequest(
    string Description,
    LocationDto Location,
    int WindowStart,
    int WindowEnd,
    string Priority,
    int EstimatedDurationMinutes,
    IReadOnlyList<string> RequiredSkills);

public record JobDto(
    Guid Id,
    string Description,
    LocationDto Location,
    int WindowStart,
    int WindowEnd,
    string Priority,
    string Status,
    int EstimatedDurationMinutes,
    IReadOnlyList<string> RequiredSkills,
    Guid? AssignedTechnicianId,
    bool IsManuallyAssigned,
    int? PlannedStart,
    int? PlannedEnd);

public record UpdateJobStatusRequest(string Status);

public record ManualAssignmentRequest(Guid TechnicianId);
