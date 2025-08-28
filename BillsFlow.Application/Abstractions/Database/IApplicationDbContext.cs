namespace BillsFlow.Application.Abstractions.Database;

#region Usings
using BillsFlow.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using BillsFlow.Domain.Entities.Bills;
#endregion

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Bill> Bills { get; }
    DbSet<BillDetail> BillDetails { get; }
}