namespace BillsFlow.Application.Customers.Update;

using BillsFlow.Application.Abstractions.Messaging;

public sealed record UpdateCustomerCommand(Guid CustomerId, Dtos.CustomerDto customerDto) :ICommand;