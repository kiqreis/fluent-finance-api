using FluentFinance.Api.Common.Api;
using FluentFinance.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace FluentFinance.Api.Routes.Identity;

public class LogoutRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapPost("/logout", HandleAsync);

  private static async Task<IResult> HandleAsync(SignInManager<User> signInManager)
  {
    await signInManager.SignOutAsync();

    return Results.Ok();
  }
}