using FluentFinance.Core.Enums;

namespace FluentFinance.Core.Models;

public class Transaction
{
  public long Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? PaidOrReceivedAt { get; set; }
  public TransactionType Type { get; set; } = TransactionType.Withdraw;
  public decimal Amount { get; set; }
  
  public long CategoryId { get; set; }
  public Category Category { get; set; } = null!;

  public string UserId { get; set; } = string.Empty;
}