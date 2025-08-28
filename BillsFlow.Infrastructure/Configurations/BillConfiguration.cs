namespace BillsFlow.Infrastructure.Configurations;

#region Usings
using BillsFlow.Domain.Entities.Bills;
using BillsFlow.Domain.Entities.Bills.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endregion

internal sealed class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    #region Constants
    private const string TableName = "bill";
    private const string DefaultDecimalType = "decimal(18,2)";
    #endregion
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(b => b.Id);

        builder.Property(b => b.TotalPrice)
            .HasConversion(ta => ta.Value, value => new Price(value))
            .HasColumnType(DefaultDecimalType);

        builder.Property(b => b.Concept)
            .HasConversion(c => c.Value, value => new Concept(value))
            .HasMaxLength(250);

        builder.Property(b => b.Status)
            .HasConversion<string>();

        builder.HasMany(b => b.Details)
            .WithOne()
            .HasForeignKey(d => d.BillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}