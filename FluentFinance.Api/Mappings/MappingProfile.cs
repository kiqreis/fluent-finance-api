using AutoMapper;
using FluentFinance.Core.Models;
using FluentFinance.Core.Responses.Categories;

namespace FluentFinance.Api.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Category, CategoryResponseDto>().ReverseMap();
  }
}