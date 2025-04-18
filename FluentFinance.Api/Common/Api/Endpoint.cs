using FluentFinance.Api.Models;
using FluentFinance.Api.Routes.Categories;
using FluentFinance.Api.Routes.Identity;
using FluentFinance.Api.Routes.Transactions;
using FluentFinance.Core.Requests.Transactions;

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

    group.MapGroup("/transactions")
      .WithTags("Transactions")
      .MapEndpoint<CreateTransactionRoute>()
      .MapEndpoint<UpdateTransactionRoute>()
      .MapEndpoint<DeleteTransactionRoute>()
      .MapEndpoint<GetByIdTransactionRoute>()
      .MapEndpoint<GetTransactionByPeriodRoute>();

    group.MapGroup("/identity")
      .WithTags("Identity")
      .MapIdentityApi<User>();

    group.MapGroup("/identity")
      .WithTags("Identity")
      .MapEndpoint<LogoutRoute>()
      .MapEndpoint<GetRolesRoute>();
  }

  private static IEndpointRouteBuilder MapEndpoint<T>(this IEndpointRouteBuilder routeBuilder) where T : IEndpoint
  {
    T.Map(routeBuilder);
    return routeBuilder;
  }
}