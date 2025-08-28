using BillsFlow.Domain.Entities.Bills;
using BillsFlow.Infrastructure.Database;

namespace BillsFlow.Infrastructure.Repositories;

internal sealed class BillDetailRepository : Repository<BillDetail>, IBillDetailRepository
{
    public BillDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}