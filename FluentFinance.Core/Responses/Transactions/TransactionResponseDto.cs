using System.ComponentModel;
using System.Text.Json.Serialization;
using FluentFinance.Core.Enums;
using FluentFinance.Core.Models;

namespace FluentFinance.Core.Responses.Transactions;

public class TransactionResponseDto
{
  public long Id { get; set; }
  public string Title { get; set; } = string.Empty;

  [DefaultValue(TransactionType.Withdraw)]
  public TransactionType Type { get; set; } = TransactionType.Withdraw;

  public decimal Amount { get; set; }
  public Category Category { get; set; } = null!;
}