namespace BillsFlow.Application.Bills.ExportBillByIdToExcel;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills.Services;
using BillsFlow.Domain.Entities.Bills;
#endregion

internal sealed class ExportBillByIdToExcelHandler : IQueryHandler<ExportBillByIdToExcelQuery, byte[]>
{
    private readonly IBillRepository _billRepository;
    private readonly IExcelExportService _excelService;

    public ExportBillByIdToExcelHandler(
        IBillRepository billRepository,
        IExcelExportService excelService)
    {
        _billRepository = billRepository;
        _excelService = excelService;
    }

    public async Task<Result<byte[]>> Handle(
        ExportBillByIdToExcelQuery request,
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);
        if (bill is null) return Result.Failure<byte[]>(BillErrors.NotFound);
        var fileBytes = _excelService.ExportBillDetailsToExcel(bill);

        return fileBytes;
    }
}