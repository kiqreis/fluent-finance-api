using AutoMapper;
using FluentFinance.Api.Data;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Models;
using FluentFinance.Core.Requests.Categories;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Categories;

namespace FluentFinance.Api.Handlers;

public class CategoryHandler(AppDbContext context, IMapper mapper) : ICategoryHandler
{
  public async Task<Response<CategoryResponseDto>> CreateAsync(CreateCategoryRequest request)
  {
    var category = new Category
    {
      UserId = request.UserId,
      Title = request.Title,
      Description = request.Description
    };

    await context.Categories.AddAsync(category);
    await context.SaveChangesAsync();

    var response = mapper.Map<CategoryResponseDto>(category);
    
    return new Response<CategoryResponseDto>(response);
  }

  public Task<Response<CategoryResponseDto>> UpdateAsync(UpdateCategoryRequest request)
  {
    throw new NotImplementedException();
  }

  public Task<Response<CategoryResponseDto>> DeleteAsync(DeleteCategoryRequest request)
  {
    throw new NotImplementedException();
  }

  public Task<Response<CategoryResponseDto>> GetByIdAsync(GetCategoryByIdRequest request)
  {
    throw new NotImplementedException();
  }

  public Task<Response<IList<CategoryResponseDto>>> GetAllAsync(GetAllCategoriesRequest request)
  {
    throw new NotImplementedException();
  }
}