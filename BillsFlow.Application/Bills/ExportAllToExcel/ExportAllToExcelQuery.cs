namespace BillsFlow.Application.Bills.ExportAllToExcel;

using BillsFlow.Application.Abstractions.Messaging;

public sealed record ExportAllToExcelQuery() : IQuery<byte[]>;