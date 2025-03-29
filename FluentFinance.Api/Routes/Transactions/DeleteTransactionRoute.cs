using FluentFinance.Api.Common.Api;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Responses.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace FluentFinance.Api.Routes.Transactions;

public class DeleteTransactionRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapDelete("/{id:int}", HandleAsync)
    .WithName("Transactions: delete")
    .WithSummary("Delete a transaction")
    .WithOrder(3)
    .Produces<TransactionResponseDto>();
  
  private static async Task<IResult> HandleAsync([FromRoute] long id, [FromServices] ITransactionHandler handler)
  {
    var result = await handler.DeleteAsync(id);

    return result.IsSuccess
      ? Results.Ok(result)
      : Results.BadRequest(result.Data);
  }
}