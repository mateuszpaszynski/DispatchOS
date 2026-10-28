using DispatchOS.Contracts.Jobs;

namespace DispatchOS.Contracts.Technicians;

public record TechnicianDto(
    Guid Id,
    string Name,
    int ShiftStart,
    int ShiftEnd,
    LocationDto CurrentLocation,
    IReadOnlyList<string> Skills);

public record CreateTechnicianRequest(
    string Name,
    int ShiftStart,
    int ShiftEnd,
    LocationDto CurrentLocation,
    IReadOnlyList<string> Skills);
