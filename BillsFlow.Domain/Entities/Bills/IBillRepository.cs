namespace BillsFlow.Domain.Entities.Bills;

using BillsFlow.Domain.Abstractions;

public interface IBillRepository : IRepository<Bill>
{
    Task<Bill?> GetByIdWithIncludeAsync(Guid id, CancellationToken cancellationToken);
}