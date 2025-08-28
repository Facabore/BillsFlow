namespace BillsFlow.Api.Controllers.Customers;

#region Usings
using BillsFlow.Api.Controllers.Shared;
using BillsFlow.Application.Customers.GetAllCustomersWithoutFilter;
using BillsFlow.Application.Customers.Delete;
using BillsFlow.Application.Customers.Dtos;
using BillsFlow.Application.Customers.Register;
using BillsFlow.Application.Customers.SearchCustomers;
using BillsFlow.Application.Customers.Update;
using BillsFlow.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
#endregion

[ApiController]
[Route("api/customer")]
public class CustomersController : ControllerBase
{
    private readonly ISender _sender;

    public CustomersController(
        ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Register(
        CustomerDto request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterCustomerCommand(request);

        Result result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(
        [FromQuery] Guid CustomerId,
        CustomerDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCustomerCommand(CustomerId, request);
        Result result = await _sender.Send(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        [FromQuery] Guid CustomerId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCustomerCommand(CustomerId);

        Result result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return Ok();
    }

    [HttpGet("search-customers")]
    public async Task<IActionResult> SearchCustomers(
        [FromQuery] SearchRequest search,
        [FromQuery] SortingRequest sortingRequest,
        [FromQuery] PaginationRequest paginationRequest,
        CancellationToken cancellationToken)
    {
        var query = new SearchCustomersQuery(
            search.SearchTerm,
            sortingRequest.SortColum,
            sortingRequest.SortOrder,
            paginationRequest.Page,
            paginationRequest.PageSize);

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }

    [HttpGet("all-without-filter")]
    public async Task<IActionResult> GetAllCustomersWithoutFilter(
        CancellationToken cancellationToken)
    {
        var query = new GetAllCustomersWithoutFilterQuery();

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure) return BadRequest(result.Error);
        return Ok(result.Value);
    }


}

