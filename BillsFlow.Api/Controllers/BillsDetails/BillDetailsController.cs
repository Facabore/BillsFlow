namespace BillsFlow.Api.Controllers.BillsDetails;

#region Usings
using BillsFlow.Application.BillsDetails.Delete;
using BillsFlow.Application.BillsDetails.Dtos;
using BillsFlow.Application.BillsDetails.GetBillDetails;
using BillsFlow.Application.BillsDetails.Register;
using BillsFlow.Application.BillsDetails.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
#endregion

[ApiController]
[Route("api/bill-details")]
public class BillDetailsController : ControllerBase
{
    private readonly ISender _sender;
    public BillDetailsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterBillDetail(
        BillDetailDto request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterBillDetailCommand(request);
        var result = await _sender.Send(command, cancellationToken);

        if(result.IsFailure) return BadRequest(result.Error);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBillDetail(
        [FromQuery] Guid id,
        BillDetailDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateBillDetailsCommand(id, request);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBillDetail(
        [FromQuery] Guid BillId,
        [FromQuery] Guid BillDetailId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteBillDetailCommand(BillId, BillDetailId);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetBillDetailsByBillId(
        [FromQuery] Guid BillId,
        CancellationToken cancellationToken)
    {
        var query = new GetBillDetailsQuery(BillId);
        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }
}

