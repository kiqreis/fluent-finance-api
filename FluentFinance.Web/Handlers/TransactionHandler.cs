using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Transactions;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Transactions;

namespace FluentFinance.Web.Handlers;

public class TransactionHandler : ITransactionHandler
{
  public Task<Response<TransactionResponseDto>> CreateAsync(CreateTransactionRequest request)
  {
    throw new NotImplementedException();
  }

  public Task<Response<TransactionResponseDto?>> DeleteAsync(long id)
  {
    throw new NotImplementedException();
  }

  public Task<Response<TransactionResponseDto?>> GetById(long id)
  {
    throw new NotImplementedException();
  }

  public Task<PagedResponse<IList<TransactionResponseDto>>> GetByPeriod(GetTransactionByPeriodRequest request)
  {
    throw new NotImplementedException();
  }

  public Task<Response<TransactionResponseDto?>> UpdateAsync(UpdateTransactionRequest request)
  {
    throw new NotImplementedException();
  }
}
