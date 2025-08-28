namespace BillsFlow.Api.Controllers.Bills;

#region Usings
using BillsFlow.Application.Bills.Create;
using BillsFlow.Application.Bills.Delete;
using BillsFlow.Application.Bills.Dtos;
using BillsFlow.Application.Bills.ExportAllToExcel;
using BillsFlow.Application.Bills.ExportBillByIdToExcel;
using BillsFlow.Application.Bills.GetById;
using BillsFlow.Application.Bills.SearchBills;
using BillsFlow.Application.Bills.Update;
using BillsFlow.Domain.Entities.Bills.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
#endregion

[ApiController]
[Route("api/bills")]
public class BillController : ControllerBase
{
    private readonly ISender _sender;
    public BillController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBill(
        BillDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateBillCommand(request);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBill(
        [FromQuery] Guid BillId,
        BillDto request,
        [FromQuery] BillStatus status,
        CancellationToken cancellationToken)
    {
        var command = new UpdateBillCommand(BillId, request, status);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBill(
        [FromQuery] Guid BillId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteBillCommand(BillId);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpGet("search-bills")]
    public async Task<IActionResult> SearchBills(
        [FromQuery] string? searchTerm,
        [FromQuery] string? sortColumn,
        [FromQuery] string? sortOrder,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new SearchBillsQuery(searchTerm, sortColumn, sortOrder, page, pageSize);

        var result = await _sender.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetBillById(
        [FromQuery] Guid BillId,
        CancellationToken cancellationToken)
    {
        var query = new GetBillByIdQuery(BillId);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportBills(CancellationToken cancellationToken)
    {
        var query = new ExportAllToExcelQuery();
        var result = await _sender.Send(query, cancellationToken);

        return File(result.Value,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "Bills.xlsx");
    }

    [HttpGet("export/{billId}")]
    public async Task<IActionResult> ExportBillDetails(Guid id, CancellationToken cancellationToken)
    {
        var query = new ExportBillByIdToExcelQuery(id);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return File(result.Value,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"detailed_bill_{id}.xlsx");
    }
}

