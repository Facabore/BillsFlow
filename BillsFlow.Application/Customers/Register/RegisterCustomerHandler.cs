namespace BillsFlow.Application.Customers.Register;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Customers;
using BillsFlow.Application.Abstractions.Clock;
using BillsFlow.Application.Customers.Dtos;
#endregion

internal sealed class RegisterCustomerHandler : ICommandHandler<RegisterCustomerCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RegisterCustomerHandler(
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>>Handle(
        RegisterCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var alreadyExistEmail = await _customerRepository.EmailExistAsync(request.CustomerDto.Email, cancellationToken);

        if(alreadyExistEmail) return Result.Failure<Guid>(CustomerErrors.EmailAlreadyExist(request.CustomerDto.Email));

        Customer customer = request.CustomerDto.ToDomain(_dateTimeProvider.UtcNow);

        _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(customer.Id);

    }
}