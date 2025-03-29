using FluentFinance.Api.Common.Api;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Transactions;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace FluentFinance.Api.Routes.Transactions;

public class GetTransactionByPeriodRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapGet("/", HandleAsync)
    .WithName("Transactions: get by period")
    .WithSummary("Get transactions by period")
    .WithOrder(5)
    .Produces<PagedResponse<TransactionResponseDto>>();

  private static async Task<IResult> HandleAsync([AsParameters] GetTransactionByPeriodRequest request,
    [FromServices] ITransactionHandler handler)
  {
    var result = await handler.GetByPeriod(request);

    return result.IsSuccess
      ? Results.Ok(result)
      : Results.BadRequest(result.Data);
  }
}