namespace BillsFlow.Application.Abstractions.Messaging;

#region Usings
using BillsFlow.Domain.Abstractions;
using MediatR;
#endregion

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}
