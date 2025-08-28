using BillsFlow.Application.Taxes.Create;
using BillsFlow.Application.Taxes.Delete;
using BillsFlow.Application.Taxes.Dtos;
using BillsFlow.Application.Taxes.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BillsFlow.Api.Controllers.Taxes;
[ApiController]
[Route("api/taxes")]
public class TaxesController : ControllerBase
{
    private readonly ISender _sender;
    public TaxesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        TaxDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTaxCommand(request);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTaxCommand(id);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var query = new GetTaxQuery();
        var result = await _sender.Send(query, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }
}

