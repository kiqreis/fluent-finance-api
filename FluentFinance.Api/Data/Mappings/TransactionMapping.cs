using FluentFinance.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentFinance.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
  public void Configure(EntityTypeBuilder<Transaction> builder)
  {
    builder.ToTable("Transactions");

    builder.HasKey(x => x.Id);

    builder.Property(x => x.Id)
      .ValueGeneratedOnAdd()
      .UseIdentityColumn();

    builder.Property(x => x.Title)
      .HasColumnType("NVARCHAR")
      .HasMaxLength(80)
      .IsRequired();

    builder.Property(x => x.Type)
      .HasColumnType("SMALLINT")
      .IsRequired();

    builder.Property(x => x.Amount)
      .HasColumnType("MONEY")
      .IsRequired();

    builder.Property(x => x.CreatedAt)
      .IsRequired();

    builder.Property(x => x.PaidOrReceivedAt)
      .IsRequired(false);

    builder.Property(x => x.UserId)
      .HasColumnType("VARCHAR")
      .HasMaxLength(160)
      .IsRequired();
  }
}