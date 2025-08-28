using BillsFlow.Application.Abstractions.Messaging;

namespace BillsFlow.Application.Customers.Delete;

public record DeleteCustomerCommand(Guid CustomerId) : ICommand;