using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Categories;
using FluentFinance.Core.Responses.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FluentFinance.Web.Pages.Categories;

public partial class ListCategoriesPage : ComponentBase
{
  public bool IsBusy { get; set; } = false;
  public IList<CategoryResponseDto> Categories { get; set; } = [];

  [Inject] public ISnackbar Snackbar { get; set; } = null!;
  [Inject] public ICategoryHandler Handler { get; set; } = null!;

  protected override async Task OnInitializedAsync()
  {
    IsBusy = true;

    try
    {
      var request = new GetAllCategoriesRequest();
      var result = await Handler.GetAllAsync(request);

      if (result.IsSuccess)
      {
        Categories = result.Data ?? [];
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
