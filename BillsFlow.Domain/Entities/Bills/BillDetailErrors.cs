using BillsFlow.Domain.Abstractions;

namespace BillsFlow.Domain.Entities.Bills;

public class BillDetailErrors
{
    public static readonly Error NotFound = new(
        "BillDetails.NotFound",
        "BillDetails was not found");
}
