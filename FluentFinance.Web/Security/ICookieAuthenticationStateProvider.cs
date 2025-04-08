using Microsoft.AspNetCore.Components.Authorization;

namespace FluentFinance.Web.Security;

public interface ICookieAuthenticationStateProvider
{
  Task<bool> CheckAuthenticatedAsync();
  Task<AuthenticationState> GetAuthenticationStateAsync();
  void NotifyAuthenticationStateChanged();
}