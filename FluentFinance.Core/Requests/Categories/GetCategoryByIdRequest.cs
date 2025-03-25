using System.ComponentModel.DataAnnotations;

namespace FluentFinance.Core.Requests.Categories;

public class GetCategoryByIdRequest : BaseRequest
{
  [Required(ErrorMessage = "Invalid id")]
  public long Id { get; set; }
}