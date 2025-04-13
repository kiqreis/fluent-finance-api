﻿using FluentFinance.Core.Handlers;
using FluentFinance.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FluentFinance.Web.Pages.Identity;

public partial class LogoutPage : ComponentBase
{
  [Inject] public ISnackbar Snackbar { get; set; } = null!;
  [Inject] public NavigationManager NavigationManager { get; set; } = null!;
  [Inject] public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
  [Inject] public IAccountHandler Handler { get; set; } = null!;

  protected override async Task OnInitializedAsync()
  {
    if (await AuthenticationStateProvider.CheckAuthenticatedAsync())
    {
      await Handler.LogoutAsync();
      await AuthenticationStateProvider.GetAuthenticationStateAsync();

      AuthenticationStateProvider.NotifyAuthenticationStateChanged();
    }

    await base.OnInitializedAsync();
  }
}
