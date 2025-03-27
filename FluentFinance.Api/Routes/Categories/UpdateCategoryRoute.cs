using FluentFinance.Api.Common.Api;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Categories;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Categories;
using Microsoft.AspNetCore.Mvc;

namespace FluentFinance.Api.Routes.Categories;

public class UpdateCategoryRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapPut("/", HandleAsync)
    .WithName("Categories: update")
    .WithSummary("Update a category")
    .WithOrder(2)
    .Produces<Response<CategoryResponseDto>>();

  private static async Task<IResult> HandleAsync([FromBody] UpdateCategoryRequest request,
    [FromServices] ICategoryHandler handler)
  {
    var result = await handler.UpdateAsync(request);

    return result.IsSuccess
      ? Results.Ok(result)
      : Results.BadRequest(result.Data);
  }
}