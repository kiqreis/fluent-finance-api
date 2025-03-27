using FluentFinance.Api.Common.Api;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Categories;
using Microsoft.AspNetCore.Mvc;

namespace FluentFinance.Api.Routes.Categories;

public class GetByIdCategoryRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapGet("/{id:int}", HandleAsync)
    .WithName("Categories: get category")
    .WithSummary("Get a category by id")
    .WithOrder(4)
    .Produces<Response<CategoryResponseDto>>();

  private static async Task<IResult> HandleAsync([FromRoute] long id, [FromServices] ICategoryHandler handler)
  {
    var result = await handler.GetByIdAsync(id);

    return result.IsSuccess
      ? Results.Ok(result)
      : Results.BadRequest(result.Data);
  }
}