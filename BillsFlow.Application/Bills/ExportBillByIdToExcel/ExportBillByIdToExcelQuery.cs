namespace BillsFlow.Application.Bills.ExportBillByIdToExcel;

using BillsFlow.Application.Abstractions.Messaging;

public record ExportBillByIdToExcelQuery(Guid BillId) : IQuery<byte[]>;