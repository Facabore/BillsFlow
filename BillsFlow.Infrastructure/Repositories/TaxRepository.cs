using BillsFlow.Domain.Entities.Taxes;
using BillsFlow.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BillsFlow.Infrastructure.Repositories;

internal sealed class TaxRepository : Repository<Tax>, ITaxRepository
{
    public TaxRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Tax?> GetByIntIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Tax>().FindAsync([id], cancellationToken);
    }
}