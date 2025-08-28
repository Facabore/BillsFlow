namespace BillsFlow.Domain.Entities.Bills;

#region Usings
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills.ValueObjects;
#endregion

public sealed class BillDetail : Entity
{
    private BillDetail(
        Guid id,
        ProductName productName,
        Quantity quantity,
        Price unitPrice)
        : base(id)
    {
        Product = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    private BillDetail()
    {
    }

    public ProductName Product { get; private set; }
    public Quantity Quantity { get; private set; }
    public Price UnitPrice { get; private set; }
    public Guid BillId { get; private set; }
    public int TaxId { get; private set; }

    public static BillDetail Create(
        ProductName product,
        Quantity quantity,
        Price unitPrice,
        int taxId)
    {
        var billDetail = new BillDetail
        (
            Guid.NewGuid(),
            product,
            quantity,
            unitPrice
        );

        billDetail.TaxId = taxId;

        return billDetail;
    }

    public void Update(
        ProductName newProductName,
        Quantity newQuantity,
        Price newUnitPrice,
        int newTaxId)
    {
        Product = newProductName;
        Quantity = newQuantity;
        UnitPrice = newUnitPrice;
        TaxId = newTaxId;
    }
}