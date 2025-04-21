using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;

namespace FluentFinance.Web.Pages.Categories;

public partial class EditCategoryPage : ComponentBase
{
  public bool IsBusy { get; set; }
  public UpdateCategoryRequest InputModel { get; set; } = new();

  [Parameter] public string Id { get; set; } = string.Empty;

  [Inject] public NavigationManager NavigationManager { get; set; } = null!;
  [Inject] public ICategoryHandler Handler { get; set; } = null!;

  protected override async Task OnInitializedAsync()
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
}