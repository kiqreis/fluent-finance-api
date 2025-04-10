using System.Net.Http.Json;
using System.Security.Claims;
using FluentFinance.Core.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;

namespace FluentFinance.Web.Security;

public class CookieAuthenticationStateProvider(IHttpClientFactory httpClientFactory)
  : AuthenticationStateProvider, ICookieAuthenticationStateProvider
{
  private bool _isAutheticated = false;
  private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

  public async Task<bool> CheckAuthenticatedAsync()
  {
    await GetAuthenticationStateAsync();

    return _isAutheticated;
  }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    _isAutheticated = false;

    var user = new ClaimsPrincipal(new ClaimsIdentity());
    var userInfo = await GetUser();

    if (userInfo is null)
      return new AuthenticationState(user);

    var claims = await GetClaims(userInfo);
    var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));

    user = new ClaimsPrincipal(id);

    _isAutheticated = true;

    return new AuthenticationState(user);
  }

  public void NotifyAuthenticationStateChanged() =>
    base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

  private async Task<User?> GetUser()
  {
    try
    {
      return await _client.GetFromJsonAsync<User?>("v1/identity/manage/info");
    }
    catch
    {
      return null;
    }
  }

  private async Task<List<Claim>> GetClaims(User user)
  {
    var claims = new List<Claim>
    {
      new(ClaimTypes.Name, user.Email),
      new(ClaimTypes.Email, user.Email)
    };

    claims.AddRange(user.Claims.Where(x => x.Key != ClaimTypes.Name && x.Key != ClaimTypes.Email)
      .Select(x => new Claim(x.Key, x.Value)));

    var roles = await _client.GetFromJsonAsync<RoleClaim[]>("v1/identity/roles");

    claims.AddRange(from role in roles ?? []
      where !string.IsNullOrEmpty(role.Type) || !string.IsNullOrEmpty(role.Value)
      select new Claim(role.Type!, role.Value!, role.ValueType, role.Issuer, role.OriginalIssuer));

    return claims;
  }
}