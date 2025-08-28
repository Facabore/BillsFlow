using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills;
using BillsFlow.Domain.Entities.Bills.ValueObjects;
using BillsFlow.Domain.Entities.Taxes;

namespace BillsFlow.Application.BillsDetails.Register;

internal sealed  class RegisterBillDetailHandler : ICommandHandler<RegisterBillDetailCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBillDetailRepository _billDetailRepository;
    private readonly IBillRepository _billRepository;
    private readonly ITaxRepository _taxRepository;

    public RegisterBillDetailHandler(
        IUnitOfWork unitOfWork,
        IBillDetailRepository billDetailRepository,
        IBillRepository billRepository,
        ITaxRepository taxRepository)
    {
        _unitOfWork = unitOfWork;
        _billDetailRepository = billDetailRepository;
        _billRepository = billRepository;
        _taxRepository = taxRepository;
    }


    public async Task<Result<Guid>> Handle(
        RegisterBillDetailCommand request,
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(request.BillDetailDto.BillId, cancellationToken);
        if (bill is null) return Result.Failure(BillErrors.NotFound) as Result<Guid>;

        var tax = await _taxRepository.GetByIntIdAsync(request.BillDetailDto.TaxId, cancellationToken);
        if (tax is null) return Result.Failure(TaxesErrors.NotFound) as Result<Guid>;

        var product = new ProductName(request.BillDetailDto.Product);
        var quantity = new Quantity(request.BillDetailDto.Quantity);
        var price = new Price(request.BillDetailDto.UnitPrice);

        var newDetail = BillDetail.Create(product, quantity, price, request.BillDetailDto.TaxId);
        bill.AddDetail(newDetail);

        _billDetailRepository.Add(newDetail);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(newDetail.Id);
    }
}