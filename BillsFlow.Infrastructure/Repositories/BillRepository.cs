using BillsFlow.Domain.Entities.Bills;
using BillsFlow.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BillsFlow.Infrastructure.Repositories;

internal sealed class BillRepository : Repository<Bill>, IBillRepository
{
    public BillRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Bill?> GetByIdWithIncludeAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Bill>()
            .Include(b => b.Details)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }
}