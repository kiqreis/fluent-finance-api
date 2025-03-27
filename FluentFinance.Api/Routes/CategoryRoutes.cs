using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Categories;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Categories;
using Microsoft.AspNetCore.Mvc;

namespace FluentFinance.Api.Routes;

public static class CategoryRoutes
{
  public static void MapCategoryRoutes(this WebApplication app)
  {
    var group = app.MapGroup("/categories");

    group.MapPost("/",
        async ([FromBody] CreateCategoryRequest request, [FromServices] ICategoryHandler handler) =>
        await handler.CreateAsync(request))
      .WithName("CreateCategory")
      .WithSummary("Create: create a new category")
      .Produces<Response<CategoryResponseDto>>();

    group.MapPut("/",
        async ([FromBody] UpdateCategoryRequest request, [FromServices] ICategoryHandler handler) =>
        await handler.UpdateAsync(request))
      .WithName("UpdateCategory")
      .WithSummary("Update: update a category")
      .Produces<Response<CategoryResponseDto>>();

    group.MapDelete("/{id:int}",
        async ([FromRoute] long id, [FromServices] ICategoryHandler handler) => await handler.DeleteAsync(id))
      .WithName("DeleteCategory")
      .WithSummary("Delete: delete a category")
      .Produces<Response<CategoryResponseDto>>();

    group.MapGet("/{id:int}",
        async ([FromRoute] long id, [FromServices] ICategoryHandler handler) => await handler.GetByIdAsync(id))
      .WithName("GetByIdCategory")
      .WithSummary("Get: get a category by id")
      .Produces<Response<CategoryResponseDto>>();

    group.MapGet("/",
        async ([AsParameters] GetAllCategoriesRequest request, [FromServices] ICategoryHandler handler) =>
        await handler.GetAllAsync(request))
      .WithName("GetAllCategories")
      .WithSummary("Get: returns all paginated categories")
      .Produces<PagedResponse<CategoryResponseDto>>();
  }
}