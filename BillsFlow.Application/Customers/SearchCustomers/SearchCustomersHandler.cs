namespace BillsFlow.Application.Customers.SearchCustomers;

#region Usings
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using BillsFlow.Domain.Entities.Customers;
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Abstractions.Pagination;
using BillsFlow.Application.Customers.Shared;
using BillsFlow.Domain.Abstractions;
#endregion

internal sealed class SearchCustomersHandler : IQueryHandler<SearchCustomersQuery, PagedList<CustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;

    public SearchCustomersHandler(
        ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<PagedList<CustomerResponse>>> Handle(
        SearchCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customerQuery = _customerRepository.GetQueryable(cancellationToken);

        if (!string.IsNullOrWhiteSpace(request.SearchParam))
        {
            string searchTerm = request.SearchParam.ToLower();
            customerQuery = customerQuery.Where(c =>
                c.PersonalInfo.FirstName.ToLower().Contains(searchTerm) ||
                c.PersonalInfo.LastName.ToLower().Contains(searchTerm));
        }

        if (request.SortOrder?.ToLower() == PaginationConfiguration.SortOrder.Descending)
        {
            customerQuery = customerQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            customerQuery = customerQuery.OrderBy(GetSortProperty(request));
        }

        var customerSearchCriteria = customerQuery
            .Select(c => new CustomerResponse
            {
                CustomerId = c.Id,
                Name = $"{c.PersonalInfo.FirstName} {c.PersonalInfo.LastName}",
                Gender = c.PersonalInfo.Gender.Value.ToString(),
                Email = c.ContactInfo.Email.Value,
                DocumentType = c.PersonalInfo.DocumentType.Value.ToString(),
                DocumentNumber = c.PersonalInfo.DocumentNumber,
                PhoneNumber = c.ContactInfo.PhoneNumber.Value.ToString(),
                CreatedOnUtc = c.CreatedAt
            });

        var customers = await PagedList<CustomerResponse>.CreateAsync(
            customerSearchCriteria,
            request.Page,
            request.PageSize);

        return customers;
    }

    private static Expression<Func<Customer, object>> GetSortProperty(SearchCustomersQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "name" => c => $"{c.PersonalInfo.FirstName} {c.PersonalInfo.LastName}",
            _ => circle => circle.Id
        };
    }

}