using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills;
using BillsFlow.Domain.Entities.Bills.ValueObjects;
using BillsFlow.Domain.Entities.Customers;

namespace BillsFlow.Application.Bills.Create;

internal sealed class CreateBillHandler : ICommandHandler<CreateBillCommand, Guid>
{
    private readonly IBillRepository _billRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;

    public CreateBillHandler(
        IBillRepository billRepository,
        IUnitOfWork unitOfWork,
        ICustomerRepository customerRepository)
    {
        _billRepository = billRepository;
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateBillCommand request,
        CancellationToken cancellationToken)
    {
        var customerExist = await _customerRepository.GetByIdAsync(request.BillDto.CustomerId, cancellationToken);
        if (customerExist is null) return Result.Failure<Guid>(CustomerErrors.NotFound);

        var bill = Domain.Entities.Bills.Bill.Create(
            request.BillDto.CustomerId,
            new Concept(request.BillDto.Concept),
            request.BillDto.IssueData);

        _billRepository.Add(bill);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(bill.Id);
    }
}