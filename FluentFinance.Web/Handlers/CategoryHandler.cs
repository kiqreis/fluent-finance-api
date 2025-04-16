using System.Net.Http.Json;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Categories;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Categories;

namespace FluentFinance.Web.Handlers;

public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
{
  private readonly HttpClient httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);

  public async Task<Response<CategoryResponseDto>> CreateAsync(CreateCategoryRequest request)
  {
    var result = await httpClient.PostAsJsonAsync("v1/categories", request);

    return await result.Content.ReadFromJsonAsync<Response<CategoryResponseDto>>()
      ?? new Response<CategoryResponseDto>(null, 400, "Failure to create category");
  }

  public async Task<Response<CategoryResponseDto?>> DeleteAsync(long id)
  {
    var result = await httpClient.DeleteAsync($"v1/categories/{id}");

    return await result.Content.ReadFromJsonAsync<Response<CategoryResponseDto?>>()
      ?? new Response<CategoryResponseDto?>(null, 400, "Failure to remove category");
  }

  public async Task<PagedResponse<IList<CategoryResponseDto>>> GetAllAsync(GetAllCategoriesRequest request) =>
    await httpClient.GetFromJsonAsync<PagedResponse<IList<CategoryResponseDto>>>($"v1/categories?pageNumber={request.PageNumber}&pageSize={request.PageSize}")
    ?? new PagedResponse<IList<CategoryResponseDto>>(null, 400, "It was not possible to obtain the categories");

  public async Task<Response<CategoryResponseDto?>> GetByIdAsync(long id) =>
    await httpClient.GetFromJsonAsync<Response<CategoryResponseDto?>>($"v1/categories/{id}")
    ?? new Response<CategoryResponseDto?>(null, 400, "It was not possible to obtain the category");

  public async Task<Response<CategoryResponseDto?>> UpdateAsync(UpdateCategoryRequest request)
  {
    var result = await httpClient.PutAsJsonAsync($"v1/categories/{request.UserId}", request);

    return await result.Content.ReadFromJsonAsync<Response<CategoryResponseDto?>>()
      ?? new Response<CategoryResponseDto?>(null, 400, "Failure to update category");
  }
}
