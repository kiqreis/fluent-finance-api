using FluentFinance.Api.Common.Api;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Transactions;
using FluentFinance.Core.Responses.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace FluentFinance.Api.Routes.Transactions;

public class CreateTransactionRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapPost("/", HandleAsync)
    .WithName("Transactions: create")
    .WithSummary("Create a new transaction")
    .WithOrder(1)
    .Produces<TransactionResponseDto>(StatusCodes.Status201Created);

  private static async Task<IResult> HandleAsync([FromBody] CreateTransactionRequest request,
    [FromServices] ITransactionHandler handler)
  {
    var result = await handler.CreateAsync(request);

    return result.IsSuccess
      ? Results.Created($"/{result.Data?.Id}", result)
      : Results.BadRequest(result.Data);
  }
}