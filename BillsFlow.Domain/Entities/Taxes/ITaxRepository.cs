using BillsFlow.Domain.Abstractions;

namespace BillsFlow.Domain.Entities.Taxes;

public interface ITaxRepository : IRepository<Tax>
{
    Task<Tax?> GetByIntIdAsync(int id, CancellationToken cancellationToken = default);
}