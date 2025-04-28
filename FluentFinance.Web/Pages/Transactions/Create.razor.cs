using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Categories;
using FluentFinance.Core.Requests.Transactions;
using FluentFinance.Core.Responses.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FluentFinance.Web.Pages.Transactions;

public partial class CreateTransactionPage : ComponentBase
{
  public bool IsBusy { get; set; }
  public CreateTransactionRequest InputModel { get; set; } = new();
  public IList<CategoryResponseDto> Categories { get; set; } = [];

  [Inject] public ITransactionHandler TransactionHandler { get; set; } = null!;
  [Inject] public ICategoryHandler CategoryHandler { get; set; } = null!;
  [Inject] public NavigationManager NavigationManager { get; set; } = null!;
  [Inject] public ISnackbar Snackbar { get; set; } = null!;

  protected override async Task OnInitializedAsync()
  {
    IsBusy = true;

    var request = new GetAllCategoriesRequest();
    var result = await CategoryHandler.GetAllAsync(request);

    try
    {
      if (result.IsSuccess)
      {
        Categories = result.Data ?? [];
        InputModel.CategoryId = Categories.FirstOrDefault()?.Id ?? 0;
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
      var result = await TransactionHandler.CreateAsync(InputModel);

      if (result.IsSuccess)
      {
        Snackbar.Add(result.Message, Severity.Success);
        NavigationManager.NavigateTo("/transactions");
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