namespace BillsFlow.Api.Controllers.Shared;

public record PaginationRequest(int Page = 1, int PageSize = 10);