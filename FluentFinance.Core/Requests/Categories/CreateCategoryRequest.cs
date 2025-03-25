using System.ComponentModel.DataAnnotations;

namespace FluentFinance.Core.Requests.Categories;

public class CreateCategoryRequest : BaseRequest
{
  [Required(ErrorMessage = "Invalid title")]
  [MaxLength(80, ErrorMessage = "Title cannot exceed 80 characters")]
  public string Title { get; set; } = string.Empty;
  
  [Required(ErrorMessage = "Invalid description")]
  [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
  public string Description { get; set; } = string.Empty;
}