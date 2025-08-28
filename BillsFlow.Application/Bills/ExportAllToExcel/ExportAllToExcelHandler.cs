namespace BillsFlow.Application.Bills.ExportAllToExcel;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills.Services;
using BillsFlow.Domain.Entities.Bills;
using Microsoft.EntityFrameworkCore;
#endregion

internal sealed class ExportAllToExcelHandler : IQueryHandler<ExportAllToExcelQuery, byte[]>
{
    private readonly IBillRepository _billRepository;
    private readonly IExcelExportService _excelService;

    public ExportAllToExcelHandler(
        IBillRepository billRepository,
        IExcelExportService excelService)
    {
        _billRepository = billRepository;
        _excelService = excelService;
    }

    public async Task<Result<byte[]>> Handle(
        ExportAllToExcelQuery request,
        CancellationToken cancellationToken)
    {
        var bills = await _billRepository.GetQueryable().ToListAsync(cancellationToken);

        var fileBytes = _excelService.ExportBillsToExcel(bills);

        return fileBytes;
    }
}