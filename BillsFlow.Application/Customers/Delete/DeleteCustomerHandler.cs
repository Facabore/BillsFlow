namespace BillsFlow.Application.Customers.Delete;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Customers;
#endregion

internal sealed class DeleteCustomerHandler : ICommandHandler<DeleteCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerHandler(
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
    }

    public async Task<Result> Handle(
        DeleteCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null) return Result.Failure(CustomerErrors.NotFound);

        _customerRepository.SoftDelete(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}