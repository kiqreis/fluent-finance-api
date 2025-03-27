using FluentFinance.Api.Common.Api;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Categories;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Categories;
using Microsoft.AspNetCore.Mvc;

namespace FluentFinance.Api.Routes.Categories;

public class GetAllCategoriesRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapGet("/", HandleAsync)
    .WithName("Categories: get all")
    .WithSummary("Get all categories paginated")
    .WithOrder(5)
    .Produces<PagedResponse<CategoryResponseDto>>();

  private static async Task<IResult> HandleAsync([AsParameters] GetAllCategoriesRequest request,
    [FromServices] ICategoryHandler handler)
  {
    var result = await handler.GetAllAsync(request);

    return result.IsSuccess
      ? Results.Ok(result)
      : Results.BadRequest(result.Data);
  }
}