using System.ComponentModel.DataAnnotations;

namespace FluentFinance.Core.Requests.Categories;

public class DeleteCategoryRequest
{
  [Required(ErrorMessage = "Invalid id")]
  public long Id { get; set; }
}