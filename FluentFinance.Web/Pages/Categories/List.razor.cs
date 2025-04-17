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
  public string SearchTerm { get; set; } = string.Empty;

  [Inject] public ISnackbar Snackbar { get; set; } = null!;
  [Inject] public IDialogService DialogService { get; set; } = null!;
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

  public async void OnDeleteButtonClicked(long id, string title)
  {
    var result = await DialogService.ShowMessageBox(
      "Look out!",
      $"When proceeding, the {title} category will be removed",
      yesText: "Remove",
      cancelText: "Cancel"
      );

    if (result is true)
      await OnDeleteAsync(id);

    StateHasChanged();
  }

  public async Task OnDeleteAsync(long id)
  {
    try
    {
      await Handler.DeleteAsync(id);

      var updatedCategories = Categories.ToList();

      updatedCategories.RemoveAll(c => c.Id == id);
      Categories = updatedCategories;

      Snackbar.Add("Category removed successfully", Severity.Success);
    }
    catch (Exception e)
    {
      Snackbar.Add(e.Message, Severity.Error);
    }
  }

  public Func<CategoryResponseDto, bool> Filter => category =>
    {
      if (string.IsNullOrEmpty(SearchTerm))
        return true;

      if (category.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
        return true;

      if (category.Description is not null && category.Description.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
        return true;

      return false;
    };
}

