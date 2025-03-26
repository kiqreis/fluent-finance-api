using System.ComponentModel.DataAnnotations;

namespace FluentFinance.Core.Requests.Categories;

public class UpdateCategoryRequest : BaseRequest
{
  [Required(ErrorMessage = "Invalid title")]
  [MaxLength(80, ErrorMessage = "Title cannot exceed 80 characters")]
  public string Title { get; set; } = string.Empty;
 
  public string? Description { get; set; }
}