using FluentFinance.Core.Requests.Account;
using FluentFinance.Core.Responses;

namespace FluentFinance.Core.Handlers;

public interface IAccountHandler
{
  Task<Response<string>> LoginAsync(LoginRequest request);
  Task<Response<string>> RegisterAsync(RegisterRequest request);
  Task LogoutAsync();
}