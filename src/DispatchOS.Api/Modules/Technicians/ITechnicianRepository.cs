using DispatchOS.Domain.Entities;

namespace DispatchOS.Api.Modules.Technicians;

public interface ITechnicianRepository
{
    Task<Technician?> GetAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Technician>> ListAsync(CancellationToken ct = default);
    Task AddAsync(Technician technician, CancellationToken ct = default);
    Task UpdateAsync(Technician technician, CancellationToken ct = default);
}
