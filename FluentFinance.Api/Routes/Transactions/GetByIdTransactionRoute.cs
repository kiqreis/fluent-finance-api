using FluentFinance.Api.Common.Api;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Responses.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace FluentFinance.Api.Routes.Transactions;

public class GetByIdTransactionRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapGet("{id:int}", HandleAsync)
    .WithName("Transaction: get by id")
    .WithSummary("Get a transaction by id")
    .WithOrder(4)
    .Produces<TransactionResponseDto>();

  private static async Task<IResult> HandleAsync([FromRoute] long id, [FromServices] ITransactionHandler handler)
  {
    var result = await handler.DeleteAsync(id);

    return result.IsSuccess
      ? Results.Ok(result)
      : Results.BadRequest(result.Data);
  }
}