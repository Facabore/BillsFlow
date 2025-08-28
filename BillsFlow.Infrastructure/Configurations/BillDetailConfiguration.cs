namespace BillsFlow.Infrastructure.Configurations;

#region Usings
using BillsFlow.Domain.Entities.Bills;
using BillsFlow.Domain.Entities.Bills.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endregion

internal sealed class BillDetailConfiguration : IEntityTypeConfiguration<BillDetail>
{
    #region Constants
    private const string TableName = "bill_detail";
    private const int MaxDefaultLength = 100;
    private const int MaxNameLength = 150;
    private const string DefaultDecimalType = "decimal(18,2)";
    #endregion
    public void Configure(EntityTypeBuilder<BillDetail> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(bd => bd.Id);

        builder.Property(d => d.Product)
            .HasConversion(p => p.Name, value => new ProductName(value))
            .HasMaxLength(MaxNameLength)
            .IsRequired();

        builder.Property(d => d.Quantity)
            .HasConversion(q => q.Value, value => new Quantity(value));

        builder.Property(d => d.UnitPrice)
            .HasConversion(p => p.Value, value => new Price(value))
            .HasColumnType(DefaultDecimalType);
    }
}