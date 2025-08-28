namespace BillsFlow.Infrastructure.Configurations;

#region Usings
using BillsFlow.Domain.Entities.Taxes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endregion

internal sealed class TaxConfiguration : IEntityTypeConfiguration<Tax>
{
    #region Constants
    private const string TableName = "tax";
    private const string DefaultDecimalType = "decimal(5,2)";
    private const int MaxDefault = 100;
    #endregion
    public void Configure(EntityTypeBuilder<Tax> builder)
    {
        builder.ToTable(TableName);
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(MaxDefault)
            .IsRequired();
        builder.Property(t => t.Percentage)
            .HasColumnType(DefaultDecimalType)
            .IsRequired();

        // Seeding
        builder.HasData(
            Tax.CreateForSeeding(1, "None", 0m),
            Tax.CreateForSeeding(2, "IVA", 19m),
            Tax.CreateForSeeding(3, "ISR", 5m)
        );
    }
}