using System.ComponentModel;
using System.Text.Json.Serialization;
using FluentFinance.Core.Enums;
using FluentFinance.Core.Models;

namespace FluentFinance.Core.Responses.Transactions;

public class TransactionResponseDto
{
  public string Title { get; set; } = string.Empty;

  [DefaultValue(TransactionType.Withdraw)]
  public TransactionType Type { get; set; } = TransactionType.Withdraw;

  [JsonIgnore] public decimal Amount { get; set; }

  [JsonPropertyName("amount")] public string FormattedAmount => $"${Amount:F2}";

  public Category Category { get; set; } = null!;
}