namespace BillsFlow.Application.Customers.Dtos;

#region Usings
using BillsFlow.Domain.Entities.Customers;
using BillsFlow.Domain.Entities.Customers.ValueObjects;
using BillsFlow.Application.Abstractions.Clock;
#endregion

internal static class CustomerExtensions
{

    public static Customer ToDomain(this Dtos.CustomerDto customerDto, DateTime utcNow)
    {
        return Customer.Create(
            new PersonalInfo(
                customerDto.FirstName,
                customerDto.LastName,
                Gender.Create(customerDto.Gender),
                DocumentType.Create(customerDto.DocumentType),
                customerDto.DocumentNumber
            ), 
            new ContactInfo(
                PhoneNumber.Create(customerDto.PhoneNumber),
                Email.Create(customerDto.Email)
            ),
            utcNow
        ); 
    }

    public static void UpdateFromDto(this Customer customer, CustomerDto customerDto)
    {
        var newPersonalInfo = new PersonalInfo(
            customerDto.FirstName,
            customerDto.LastName,
            Gender.Create(customerDto.Gender),
            DocumentType.Create(customerDto.DocumentType),
            customerDto.DocumentNumber);

        var newContactInfo = new ContactInfo(
            PhoneNumber.Create(customerDto.PhoneNumber),
            Email.Create(customerDto.Email)
        );

        customer.Update(newPersonalInfo, newContactInfo);
    }

    public static Dtos.CustomerDto ToDto(this Customer customer)
    {
        return new Dtos.CustomerDto
        {
            FirstName = customer.PersonalInfo.FirstName,
            LastName = customer.PersonalInfo.LastName,
            Email = customer.ContactInfo.Email.Value,
            PhoneNumber = customer.ContactInfo.PhoneNumber.Value,
            Gender = customer.PersonalInfo.Gender.Value.ToString(),
            DocumentType = customer.PersonalInfo.DocumentType.Value.ToString(),
            DocumentNumber = customer.PersonalInfo.DocumentNumber
        };
    }
}