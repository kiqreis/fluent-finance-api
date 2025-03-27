using AutoMapper;
using FluentFinance.Api.Data;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Models;
using FluentFinance.Core.Requests.Categories;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Categories;
using Microsoft.EntityFrameworkCore;

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

    return new Response<CategoryResponseDto>(response, 201, "Category created successfully");
  }

  public async Task<Response<CategoryResponseDto?>> UpdateAsync(UpdateCategoryRequest request)
  {
    var category = await context.Categories.FirstOrDefaultAsync(c => c.UserId == request.UserId);

    if (category is null)
      return new Response<CategoryResponseDto?>(null, 404, "Category not found");

    category.Title = request.Title;
    category.Description = request.Description;

    context.Categories.Update(category);
    await context.SaveChangesAsync();

    var response = mapper.Map<CategoryResponseDto>(category);

    return new Response<CategoryResponseDto?>(response, message: "Category updated successfully");
  }

  public async Task<Response<CategoryResponseDto?>> DeleteAsync(long id)
  {
    var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);

    if (category is null)
      return new Response<CategoryResponseDto?>(null, 404, "Category not found");

    context.Categories.Remove(category);
    await context.SaveChangesAsync();

    var response = mapper.Map<CategoryResponseDto>(category);

    return new Response<CategoryResponseDto?>(response, message: "Category deleted successfully");
  }

  public async Task<Response<CategoryResponseDto?>> GetByIdAsync(long id)
  {
    var category = await context.Categories.FindAsync(id);

    if (category is null)
      return new Response<CategoryResponseDto?>(null, 404, "Category not found");

    var response = mapper.Map<CategoryResponseDto>(category);

    return new Response<CategoryResponseDto?>(response, message: "Category found successfully");
  }

  public async Task<PagedResponse<IList<CategoryResponseDto>>> GetAllAsync(GetAllCategoriesRequest request)
  {
    var query = context.Categories.AsNoTracking();

    var categories = await query
      .Skip((request.PageNumber - 1) * request.PageSize)
      .Take(request.PageSize)
      .ToListAsync();

    var count = await query.CountAsync();

    var response = mapper.Map<IList<CategoryResponseDto>>(categories);

    return new PagedResponse<IList<CategoryResponseDto>>(response, count, request.PageNumber, request.PageSize);
  }
}