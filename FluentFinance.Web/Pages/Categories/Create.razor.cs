using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FluentFinance.Web.Pages.Categories;

public partial class CreatCategoryPage : ComponentBase
{
  public bool IsBusy { get; set; } = false;
  public CreateCategoryRequest InputModel { get; set; } = new();

  [Inject] public ICategoryHandler Handler { get; set; } = null!;
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
        NavigationManager.NavigateTo("/categories");
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
