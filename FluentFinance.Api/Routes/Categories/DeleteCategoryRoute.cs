using FluentFinance.Api.Common.Api;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Categories;
using Microsoft.AspNetCore.Mvc;

namespace FluentFinance.Api.Routes.Categories;

public class DeleteCategoryRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapDelete("/{id:int}", HandleAsync)
    .WithName("Categories: delete")
    .WithSummary("Delete a category")
    .WithOrder(3)
    .Produces<Response<CategoryResponseDto>>();

  private static async Task<IResult> HandleAsync([FromRoute] long id, [FromServices] ICategoryHandler handler)
  {
    var result = await handler.DeleteAsync(id);

    return result.IsSuccess
      ? Results.Ok(result)
      : Results.BadRequest(result.Data);
  }
}