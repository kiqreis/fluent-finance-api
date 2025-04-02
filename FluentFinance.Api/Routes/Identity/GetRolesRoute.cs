using System.Security.Claims;
using FluentFinance.Api.Common.Api;

namespace FluentFinance.Api.Routes.Identity;

public class GetRolesRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapGet("/roles", Handle);

  private static Task<IResult> Handle(ClaimsPrincipal user)
  {
    if (user.Identity is null || !user.Identity.IsAuthenticated)
      return Task.FromResult(Results.Unauthorized());

    var identity = (ClaimsIdentity)user.Identity;

    var roles = identity.FindAll(identity.RoleClaimType)
      .Select(claim => new
      {
        claim.Issuer,
        claim.OriginalIssuer,
        claim.Type,
        claim.Value,
        claim.ValueType
      });

    return Task.FromResult<IResult>(TypedResults.Json(roles));
  }
}