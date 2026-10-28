using DispatchOS.Domain.Entities;

namespace DispatchOS.Api.Modules.Jobs;

public interface IJobRepository
{
    Task<Job?> GetAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Job>> ListAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Job>> ListByTechnicianAsync(Guid technicianId, CancellationToken ct = default);
    Task AddAsync(Job job, CancellationToken ct = default);
    Task UpdateAsync(Job job, CancellationToken ct = default);
}
