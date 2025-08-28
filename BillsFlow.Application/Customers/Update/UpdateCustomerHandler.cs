namespace BillsFlow.Application.Customers.Update;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Customers.Dtos;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Customers;
#endregion

internal sealed class UpdateCustomerHandler : ICommandHandler<UpdateCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerHandler(
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
    }

    public async Task<Result> Handle(
        UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null) return Result.Failure(CustomerErrors.NotFound);

        customer.UpdateFromDto(request.customerDto);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}