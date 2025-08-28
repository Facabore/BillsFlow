using BillsFlow.Domain.Entities.Customers;
using BillsFlow.Domain.Entities.Customers.ValueObjects;
using BillsFlow.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BillsFlow.Infrastructure.Repositories;

internal sealed class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<bool> EmailExistAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Customer>()
            .AnyAsync(customer => customer.ContactInfo.Email == Email.Create(email), cancellationToken);
    }
}