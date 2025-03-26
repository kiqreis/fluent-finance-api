using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Categories;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Categories;

namespace FluentFinance.Api.Routes;

public static class CategoryRoutes
{
  public static void MapCategoryRoutes(this WebApplication app)
  {
    var group = app.MapGroup("/categories");

    group.MapPost("/",
        async (CreateCategoryRequest request, ICategoryHandler handler) => await handler.CreateAsync(request))
      .WithName("CreateCategory")
      .WithSummary("Create: create a new category")
      .Produces<Response<CategoryResponseDto>>();
  }
}