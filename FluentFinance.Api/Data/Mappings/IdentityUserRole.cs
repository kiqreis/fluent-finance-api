using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentFinance.Api.Data.Mappings;

public class IdentityUserRole : IEntityTypeConfiguration<IdentityUserRole<long>>
{
  public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
  {
    builder.ToTable("IdentityUserRole");

    builder.HasKey(x => new
    {
      x.UserId,
      x.RoleId
    });
  }
}