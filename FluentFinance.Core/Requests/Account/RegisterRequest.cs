using System.ComponentModel.DataAnnotations;

namespace FluentFinance.Core.Requests.Account;

public class RegisterRequest : BaseRequest
{
  [Required]
  [EmailAddress(ErrorMessage = "Invalid email")]
  public string Email { get; set; } = string.Empty;
  
  [Required(ErrorMessage = "Invalid password")]
  public string Password { get; set; } = string.Empty;
}