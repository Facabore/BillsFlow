namespace BillsFlow.Application.Customers.GetAllCustomersWithoutFilter;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Customers.Shared;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Customers;
#endregion



internal sealed class GetAllCustomersWithoutFilterHandler : IQueryHandler<GetAllCustomersWithoutFilterQuery, IEnumerable<CustomerWithoutFilterResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    public GetAllCustomersWithoutFilterHandler(
        ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<Result<IEnumerable<CustomerWithoutFilterResponse>>> Handle(
        GetAllCustomersWithoutFilterQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllWithDeleteAsync(cancellationToken);

        var customerResponses = customers.Select(c => new CustomerWithoutFilterResponse()
        {
            CustomerId = c.Id,
            Name = $"{c.PersonalInfo.FirstName} {c.PersonalInfo.LastName}",
            Gender = c.PersonalInfo.Gender.Value.ToString(),
            Email = c.ContactInfo.Email.Value,
            DocumentType = c.PersonalInfo.DocumentType.Value.ToString(),
            DocumentNumber = c.PersonalInfo.DocumentNumber,
            PhoneNumber = c.ContactInfo.PhoneNumber.Value.ToString(),
            CreatedOnUtc = c.CreatedAt,
            IsDeleted = c.IsDeleted
        }).ToList();

        return customerResponses;
    }
}