using FluentFinance.Api.Routes.Categories;

namespace FluentFinance.Api.Common.Api;

public static class Endpoint
{
  public static void MapEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/v1");

    group.MapGroup("/categories")
      .WithTags("Categories")
      .MapEndpoint<CreateCategoryRoute>()
      .MapEndpoint<UpdateCategoryRoute>()
      .MapEndpoint<DeleteCategoryRoute>()
      .MapEndpoint<GetByIdCategoryRoute>()
      .MapEndpoint<GetAllCategoriesRoute>();
  }

  private static IEndpointRouteBuilder MapEndpoint<T>(this IEndpointRouteBuilder routeBuilder) where T : IEndpoint
  {
    T.Map(routeBuilder);
    return routeBuilder;
  }
}