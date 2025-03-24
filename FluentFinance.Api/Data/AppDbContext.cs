using System.Reflection;
using FluentFinance.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FluentFinance.Api.Data;

public class AppDbContext : DbContext
{
  public DbSet<Category> Categories { get; set; }
  public DbSet<Transaction> Transactions { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}