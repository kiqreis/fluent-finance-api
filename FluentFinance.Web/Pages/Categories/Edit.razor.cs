using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FluentFinance.Web.Pages.Categories;

public partial class EditCategoryPage : ComponentBase
{
  public bool IsBusy { get; set; }
  public UpdateCategoryRequest InputModel { get; set; } = new();

  [Parameter] public string Id { get; set; } = string.Empty;

  [Inject] public ISnackbar Snackbar { get; set; } = null!;
  [Inject] public NavigationManager NavigationManager { get; set; } = null!;
  [Inject] public ICategoryHandler Handler { get; set; } = null!;

  protected override async Task OnInitializedAsync()
  {
    IsBusy = true;

    try
    {
      var result = await Handler.GetByIdAsync(long.Parse(Id));

      if (result.IsSuccess)
      {
        InputModel = new UpdateCategoryRequest
        {
          Title = result.Data!.Title,
          Description = result.Data!.Description
        };
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

  public async Task OnValidSubmitAsync()
  {
    IsBusy = true;

    try
    {
      var result = await Handler.UpdateAsync(InputModel);

      if (result.IsSuccess)
      {
        Snackbar.Add("Successful updated category", Severity.Success);

        NavigationManager.NavigateTo("/categories");
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