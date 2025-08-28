namespace BillsFlow.Infrastructure.Configurations;

#region Usings
using BillsFlow.Domain.Entities.Customers;
using BillsFlow.Domain.Entities.Customers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
#endregion


internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    #region Constants
    private const string TableName = "customers";
    private const int MaxDefaultLength = 100;
    private const int PhoneNumberMaxLength = 15;
    private const int MaxDocumentTypeLength = 40;
    private const int MaxDocumentNumberLength = 20;
    private const int MaxGenderLength = 10;
    private const int MaxEmailLength = 100;
    #endregion
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(TableName);
        builder.HasKey(c => c.Id);

        #region PersonalInfo Configuration
        builder.OwnsOne(customer => customer.PersonalInfo, personalInfo =>
        {
            personalInfo.Property(pi => pi.FirstName)
                .HasMaxLength(MaxDefaultLength);
            personalInfo.Property(pi => pi.LastName)
                .HasMaxLength(MaxDefaultLength);
            personalInfo.Property(pi => pi.Gender)
                .HasConversion(
                    gender => gender.Value,
                    gender => Gender.Create(gender))
                .HasMaxLength(MaxGenderLength);
            personalInfo.Property(pi => pi.DocumentType)
                .HasConversion(
                    documentType => documentType.Value,
                    documentType => DocumentType.Create(documentType))
                .HasMaxLength(MaxDocumentTypeLength);
            personalInfo.Property(pi => pi.DocumentNumber)
                .HasMaxLength(MaxDocumentNumberLength);
        });
        #endregion

        #region ContactInfo configuration
        builder.OwnsOne(customer => customer.ContactInfo, contactInfo =>
        {
            contactInfo.Property(ci => ci.PhoneNumber).HasConversion(
                    phoneNumber => phoneNumber.Value,
                    phoneNumber => PhoneNumber.Create(phoneNumber))
                .HasMaxLength(PhoneNumberMaxLength);
            contactInfo.Property(ci => ci.Email).HasConversion(
                    email => email.Value,
                    email => Email.Create(email))
                .HasMaxLength(MaxEmailLength);
        });
        #endregion
    }
}