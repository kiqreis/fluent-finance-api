using System.Net.Http.Json;
using System.Text;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Account;
using FluentFinance.Core.Responses;

namespace FluentFinance.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
  private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
  
  public async Task<Response<string?>> LoginAsync(LoginRequest request)
  {
    var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);

    return result.IsSuccessStatusCode
      ? new Response<string?>("Login successfully", 200, "Login successfully")
      : new Response<string?>(null, 400, "Unable to login");
  }

  public async Task<Response<string?>> RegisterAsync(RegisterRequest request)
  {
    var result = await _client.PostAsJsonAsync("v1/identity/register", request);

    return result.IsSuccessStatusCode
      ? new Response<string?>("Registration completed successfully", 201, "Registration completed successfully")
      : new Response<string?>(null, 400, "It was not possible to complete your registration");
  }

  public async Task LogoutAsync()
  {
    var content = new StringContent("{}", Encoding.UTF8, "application/json");

    await _client.PostAsJsonAsync("v1/identity/logout", content);
  }
}