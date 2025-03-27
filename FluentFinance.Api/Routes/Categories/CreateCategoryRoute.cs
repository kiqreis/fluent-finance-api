using FluentFinance.Api.Common.Api;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Categories;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Categories;
using Microsoft.AspNetCore.Mvc;

namespace FluentFinance.Api.Routes.Categories;

public class CreateCategoryRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapPost("/", HandleAsync)
    .WithName("Categories: create")
    .WithSummary("Create a new category")
    .WithOrder(1)
    .Produces<Response<CategoryResponseDto>>(StatusCodes.Status201Created);

  private static async Task<IResult> HandleAsync([FromBody] CreateCategoryRequest request,
    [FromServices] ICategoryHandler handler)
  {
    var result = await handler.CreateAsync(request);

    return result.IsSuccess
      ? Results.Created($"/{result.Data?.Id}", result)
      : Results.BadRequest(result.Data);
  }
}