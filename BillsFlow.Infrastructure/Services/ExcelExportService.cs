namespace BillsFlow.Infrastructure.Services;

#region Usings
using BillsFlow.Domain.Entities.Bills.Services;
using BillsFlow.Domain.Entities.Bills;
using ClosedXML.Excel;
#endregion

public class ExcelExportService : IExcelExportService
{
    public byte[] ExportBillsToExcel(IEnumerable<Bill> bills)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Bills");

        // Headers
        worksheet.Cell(1, 1).Value = "ID Bill";
        worksheet.Cell(1, 2).Value = "ID Customer";
        worksheet.Cell(1, 3).Value = "Concept";
        worksheet.Cell(1, 4).Value = "Issue Date";
        worksheet.Cell(1, 5).Value = "State";
        worksheet.Cell(1, 6).Value = "Total";
        worksheet.Row(1).Style.Font.Bold = true;

        // Data
        int currentRow = 2;
        foreach (var bill in bills)
        {
            worksheet.Cell(currentRow, 1).Value = bill.Id.ToString();
            worksheet.Cell(currentRow, 2).Value = bill.CustomerId.ToString();
            worksheet.Cell(currentRow, 3).Value = bill.Concept.Value;
            worksheet.Cell(currentRow, 4).Value = bill.IssueDate;
            worksheet.Cell(currentRow, 5).Value = bill.Status.ToString();
            worksheet.Cell(currentRow, 6).Value = bill.TotalPrice.Value;
            currentRow++;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public byte[] ExportBillDetailsToExcel(Bill bill)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add($"Bills Detail {bill.Id}");

        // Headers
        worksheet.Cell(1, 1).Value = "ID Detail";
        worksheet.Cell(1, 2).Value = "Product";
        worksheet.Cell(1, 3).Value = "Quantity";
        worksheet.Cell(1, 4).Value = "Unit price";
        worksheet.Row(1).Style.Font.Bold = true;

        // Data
        int currentRow = 2;
        foreach (var detail in bill.Details)
        {
            worksheet.Cell(currentRow, 1).Value = detail.Id.ToString();
            worksheet.Cell(currentRow, 2).Value = detail.Product.Name;
            worksheet.Cell(currentRow, 3).Value = detail.Quantity.Value;
            worksheet.Cell(currentRow, 4).Value = detail.UnitPrice.Value;
            currentRow++;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}