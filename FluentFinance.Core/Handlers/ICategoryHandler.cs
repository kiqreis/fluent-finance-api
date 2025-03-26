using FluentFinance.Core.Requests.Categories;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Categories;

namespace FluentFinance.Core.Handlers;

public interface ICategoryHandler
{
  Task<Response<CategoryResponseDto>> CreateAsync(CreateCategoryRequest request);
  Task<Response<CategoryResponseDto>> UpdateAsync(UpdateCategoryRequest request);
  Task<Response<CategoryResponseDto>> DeleteAsync(DeleteCategoryRequest request);
  Task<Response<CategoryResponseDto>> GetByIdAsync(GetCategoryByIdRequest request);
  Task<Response<IList<CategoryResponseDto>>> GetAllAsync(GetAllCategoriesRequest request);
}