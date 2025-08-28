namespace BillsFlow.Application.Abstractions.Messaging;

#region Usings
using BillsFlow.Domain.Abstractions;
using MediatR;
#endregion

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
