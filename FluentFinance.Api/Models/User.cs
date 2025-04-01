using Microsoft.AspNetCore.Identity;

namespace FluentFinance.Api.Models;

public class User : IdentityUser<long>
{
  public List<IdentityRole<long>>? Roles { get; set; }
}