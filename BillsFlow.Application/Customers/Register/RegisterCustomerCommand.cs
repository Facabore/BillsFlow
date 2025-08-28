namespace BillsFlow.Application.Customers.Register;

using BillsFlow.Application.Abstractions.Messaging;

public record RegisterCustomerCommand(Dtos.CustomerDto CustomerDto) : ICommand<Guid>;