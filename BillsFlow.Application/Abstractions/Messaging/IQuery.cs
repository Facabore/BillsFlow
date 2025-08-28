namespace BillsFlow.Application.Abstractions.Messaging;

#region Usings
using BillsFlow.Domain.Abstractions;
using MediatR;
#endregion

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
