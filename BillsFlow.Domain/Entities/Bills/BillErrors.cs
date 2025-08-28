using BillsFlow.Domain.Abstractions;

namespace BillsFlow.Domain.Entities.Bills;

public static class BillErrors
{
    public static readonly Error NotFound = new(
        "Bill.NotFound",
        "Bill was not found");
}