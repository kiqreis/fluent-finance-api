using System.Security.Claims;
using FluentFinance.Api.Common.Api;
using FluentFinance.Core.Models.Account;

namespace FluentFinance.Api.Routes.Identity;

public class GetRolesRoute : IEndpoint
{
  public static void Map(IEndpointRouteBuilder routeBuilder) => routeBuilder.MapGet("/roles", Handle)
    .RequireAuthorization();

  private static Task<IResult> Handle(ClaimsPrincipal user)
  {
    if (user.Identity is null || !user.Identity.IsAuthenticated)
      return Task.FromResult(Results.Unauthorized());

    var identity = (ClaimsIdentity)user.Identity;

    var roles = identity.FindAll(identity.RoleClaimType)
      .Select(claim => new RoleClaim
      {
        Issuer = claim.Issuer,
        OriginalIssuer = claim.OriginalIssuer,
        Type = claim.Type,
        Value = claim.Value,
        ValueType = claim.ValueType
      });

    return Task.FromResult<IResult>(TypedResults.Json(roles));
  }
}