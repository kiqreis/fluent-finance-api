using System.ComponentModel.DataAnnotations;
using FluentFinance.Core.Enums;

namespace FluentFinance.Core.Requests.Transactions;

public class CreateTransactionRequest : BaseRequest
{
  [Required(ErrorMessage = "Invalid title")]
  public string Title { get; set; } = string.Empty;

  [Required(ErrorMessage = "Invalid title")]
  public TransactionType Type { get; set; } = TransactionType.Withdraw;
  
  [Required(ErrorMessage = "Invalid amount")]
  public decimal Amount { get; set; }
  
  [Required(ErrorMessage = "Invalid category")]
  public long CategoryId { get; set; }
  
  [Required(ErrorMessage = "Invalid date")]
  public DateTime? PaidOrReceivedAt { get; set; }
}