using BillsFlow.Domain.Abstractions;

namespace BillsFlow.Domain.Entities.Customers;

public static class CustomerErrors
{
    public static readonly Error NotFound = new(
        "Customer.NotFound",
        "Customer was not found");

    public static Error EmailAlreadyExist(string email) => new(
        "Customer.EmailAlreadyExists",
        $"The email '{email}' is already in use by another customer.");

    public static Error ParamNotFound = new(
        "Customer.ParamNotFound",
        "The parameter than need search was not found");
}