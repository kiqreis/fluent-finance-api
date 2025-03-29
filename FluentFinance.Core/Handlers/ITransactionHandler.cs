using FluentFinance.Core.Requests.Transactions;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Transactions;

namespace FluentFinance.Core.Handlers;

public interface ITransactionHandler
{
  Task<Response<TransactionResponseDto>> CreateAsync(CreateTransactionRequest request);
  Task<Response<TransactionResponseDto?>> UpdateAsync(UpdateTransactionRequest request);
  Task<Response<TransactionResponseDto?>> DeleteAsync(long id);
  Task<Response<TransactionResponseDto?>> GetById(long id);
  Task<PagedResponse<IList<TransactionResponseDto>>> GetByPeriod(GetTransactionByPeriodRequest request);
}