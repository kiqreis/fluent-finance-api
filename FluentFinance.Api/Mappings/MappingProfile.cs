using AutoMapper;
using FluentFinance.Core.Models;
using FluentFinance.Core.Responses.Categories;
using FluentFinance.Core.Responses.Transactions;

namespace FluentFinance.Api.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Category, CategoryResponseDto>().ReverseMap();
    CreateMap<Transaction, TransactionResponseDto>().ReverseMap();
  }
}