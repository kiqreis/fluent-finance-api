using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FluentFinance.Web.Pages.Transactions;

public partial class CreateTransactionPage : ComponentBase
{
  public bool IsBusy { get; set; }
  public CreateTransactionRequest InputModel { get; set; } = new();

  [Inject] public ITransactionHandler Handler { get; set; } = null!;
  [Inject] public NavigationManager NavigationManager { get; set; } = null!;
  [Inject] public ISnackbar Snackbar { get; set; } = null!;

  public async Task OnValidSubmitAsync()
  {
    IsBusy = true;

    try
    {
      var result = await Handler.CreateAsync(InputModel);

      if (result.IsSuccess)
      {
        Snackbar.Add(result.Message, Severity.Success);
        NavigationManager.NavigateTo("/");
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
    {
      IsBusy = false;
    }
  }
  
}