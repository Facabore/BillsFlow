namespace BillsFlow.Domain.Entities.Bills.Services;

public interface IExcelExportService
{
    byte[] ExportBillsToExcel(IEnumerable<Bill> bills);
    byte[] ExportBillDetailsToExcel(Bill bill);
}