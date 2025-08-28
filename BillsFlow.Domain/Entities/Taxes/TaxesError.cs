namespace BillsFlow.Domain.Entities.Taxes;

using BillsFlow.Domain.Abstractions;

public static class TaxesErrors
{
    public static readonly Error NotFound = new(
        "Tax.NotFound",
        "Tax was not found");

    public static Error LessThanZero = new(
        "Tax.LessThanZero",
        "The Tax cant be less than zero");
}