using Azure;
using FluentFinance.Api.Common.Api;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Transactions;
using FluentFinance.Core.Responses.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace FluentFinance.Api.Routes.Transactions;

public class UpdateTransactionRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapPut("/", HandleAsync)
    .WithName("Transactions: update")
    .WithSummary("Update a transaction")
    .WithOrder(2)
    .Produces<Response<TransactionResponseDto>>();

  private static async Task<IResult> HandleAsync([FromBody] UpdateTransactionRequest request,
    [FromServices] ITransactionHandler handler)
  {
    var result = await handler.UpdateAsync(request);

    return result.IsSuccess
      ? Results.Ok(result)
      : Results.BadRequest(result.Data);
  }
}