using System.Text.Json.Serialization;

namespace FluentFinance.Core.Responses.Categories;

public class CategoryResponseDto
{
  public long Id { get; set; }
  public string Title { get; set; } = string.Empty;

  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
  public string? Description
  {
    get => string.IsNullOrWhiteSpace(_description) ? null : _description;
    set => _description = value;
  }

  private string? _description;
}