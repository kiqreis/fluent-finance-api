using FluentFinance.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentFinance.Api.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    builder.ToTable("Categories");

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id)
      .ValueGeneratedOnAdd()
      .UseIdentityColumn();

    builder.Property(x => x.Title)
      .HasColumnType("NVARCHAR")
      .HasMaxLength(80)
      .IsRequired();

    builder.Property(x => x.Description)
      .HasColumnType("NVARCHAR")
      .HasMaxLength(255)
      .IsRequired(false);

    builder.Property(x => x.UserId)
      .HasColumnType("VARCHAR")
      .HasMaxLength(160)
      .IsRequired();
  }
}