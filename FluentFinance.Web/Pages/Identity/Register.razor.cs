using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Account;
using FluentFinance.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FluentFinance.Web.Pages.Identity;

public partial class RegisterPage : ComponentBase
{
  [Inject] public ISnackbar Snackbar { get; set; } = null!;

  [Inject] public NavigationManager NavigationManager { get; set; } = null!;

  [Inject] public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

  [Inject] public IAccountHandler Handler { get; set; } = null!;

  public bool IsBusy { get; set; }
  public RegisterRequest InputModel { get; set; } = new();

  protected override async Task OnInitializedAsync()
  {
    var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
    var user = authenticationState.User;

    if (user.Identity is { IsAuthenticated: true })
      NavigationManager.NavigateTo("/");
  }

  public async Task OnValidSubmitAsync()
  {
    IsBusy = true;

    try
    {
      var result = await Handler.RegisterAsync(InputModel);

      if (result.IsSuccess)
      {
        Snackbar.Add(result.Message, Severity.Success);

        NavigationManager.NavigateTo("/login");
      }
      else
      {
        Snackbar.Add(result.Message, Severity.Error);
      }
    }
    catch (Exception e)
    {
      Snackbar.Add(e.Message, Severity.Error);
    }
    finally
    { IsBusy = false; }
  }
}