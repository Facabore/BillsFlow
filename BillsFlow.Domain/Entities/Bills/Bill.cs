namespace BillsFlow.Domain.Entities.Bills;

#region Usings
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills.ValueObjects;
#endregion

public sealed class Bill : Entity
{
    private readonly List<BillDetail> _details = new();

    private Bill(
        Guid id,
        Guid customerId,
        Concept concept,
        DateTime issueDate)
        : base(id)
    {
        CustomerId = customerId;
        Concept = concept;
        IssueDate = issueDate;
    }
    private Bill() { }


    public Guid CustomerId { get; private set; }
    public Concept Concept { get; private set; }
    public DateTime IssueDate { get; private set; }
    public Price TotalPrice { get; private set; }
    public BillStatus Status { get; private set; }

    public IReadOnlyList<BillDetail> Details => _details.AsReadOnly();

    public static Bill Create(Guid customerId, Concept concept, DateTime issueDate)
    {
        var bill = new Bill(
            Guid.NewGuid(),
            customerId, concept,
            issueDate);
        bill.Status = BillStatus.Draft;
        bill.TotalPrice = new Price(0);
        return bill;
    }

    public void AddDetail(
        BillDetail newDetail)
    {
        _details.Add(newDetail);
        RecalculateTotalPrice();
    }

    public void UpdateDetail(
        Guid detailId,
        string newProductName,
        int newQuantity,
        decimal newUnitPrice,
        int taxId)
    {
        var detail = _details.FirstOrDefault(d => d.Id == detailId);
        if (detail is null) return;
        
        detail.Update(
            new ProductName(newProductName),
            new Quantity(newQuantity),
            new Price(newUnitPrice),
            taxId);

        RecalculateTotalPrice();

    }
    public void RemoveDetail(Guid detailId)
    {
        var detail = _details.FirstOrDefault(d => d.Id == detailId);
        if (detail is null) return;

        _details.Remove(detail);
        RecalculateTotalPrice();

    }

    private void RecalculateTotalPrice()
    {

        TotalPrice = new Price(_details.Sum(d => d.Quantity.Value * d.UnitPrice.Value));
    }

    public void Update(
        Concept newConcept,
        DateTime newIssueDate,
        BillStatus status)
    {

        Concept = newConcept;
        IssueDate = newIssueDate;
        Status = status;
    }

    public void MarkAsSent() => Status = BillStatus.Sent;
    public void MarkAsPaid() => Status = BillStatus.Paid;
}