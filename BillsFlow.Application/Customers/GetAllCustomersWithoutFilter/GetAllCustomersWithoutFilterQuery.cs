namespace BillsFlow.Application.Customers.GetAllCustomersWithoutFilter;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Customers.Shared;
#endregion

public record GetAllCustomersWithoutFilterQuery() : IQuery<IEnumerable<CustomerWithoutFilterResponse>>;