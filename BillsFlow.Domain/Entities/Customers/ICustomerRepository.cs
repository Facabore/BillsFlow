using System.Reflection.Metadata;
using BillsFlow.Domain.Abstractions;

namespace BillsFlow.Domain.Entities.Customers;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<bool> EmailExistAsync(string email, CancellationToken cancellationToken);
}