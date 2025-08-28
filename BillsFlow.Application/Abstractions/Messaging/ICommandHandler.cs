namespace BillsFlow.Application.Abstractions.Messaging;

#region Usings
using BillsFlow.Domain.Abstractions;
using MediatR;
#endregion

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
