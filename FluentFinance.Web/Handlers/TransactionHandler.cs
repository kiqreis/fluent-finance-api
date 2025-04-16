using System.Net.Http.Json;
using FluentFinance.Core.Common.Extensions;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Requests.Transactions;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Transactions;

namespace FluentFinance.Web.Handlers;

public class TransactionHandler(IHttpClientFactory httpClientFactory) : ITransactionHandler
{
  private readonly HttpClient httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);

  public async Task<Response<TransactionResponseDto>> CreateAsync(CreateTransactionRequest request)
  {
    var result = await httpClient.PostAsJsonAsync("v1/transactions", request);

    return await result.Content.ReadFromJsonAsync<Response<TransactionResponseDto>>()
      ?? new Response<TransactionResponseDto>(null, 400, "Failure to create transaction");
  }

  public async Task<Response<TransactionResponseDto?>> DeleteAsync(long id)
  {
    var result = await httpClient.DeleteAsync($"v1/transactions/{id}");

    return await result.Content.ReadFromJsonAsync<Response<TransactionResponseDto?>>()
      ?? new Response<TransactionResponseDto?>(null, 400, "Failure to delete transaction");
  }

  public async Task<Response<TransactionResponseDto?>> GetById(long id) =>
    await httpClient.GetFromJsonAsync<Response<TransactionResponseDto?>>($"v1/transactions/{id}")
    ?? new Response<TransactionResponseDto?>(null, 400, "It was not possible to obtain the transaction");

  public async Task<PagedResponse<IList<TransactionResponseDto>>> GetByPeriod(GetTransactionByPeriodRequest request)
  {
    const string format = "yyyy-MM-dd";

    var startDate = request.StartDate is not null ? request.StartDate.Value.ToString(format) : DateTime.Now.GetFirstDay().ToString(format);
    var endDate = request.EndDate is not null ? request.EndDate.Value.ToString(format) : DateTime.Now.GetLastDay().ToString(format);
    var url = $"v1/transactions?startDate={startDate}&endDate={endDate}";

    return await httpClient.GetFromJsonAsync<PagedResponse<IList<TransactionResponseDto>>>(url)
      ?? new PagedResponse<IList<TransactionResponseDto>>(null, 400, "It was not possible to obtain the transactions");
  }

  public async Task<Response<TransactionResponseDto?>> UpdateAsync(UpdateTransactionRequest request)
  {
    var result = await httpClient.PutAsJsonAsync($"v1/transactions/{request.UserId}", request);

    return await result.Content.ReadFromJsonAsync<Response<TransactionResponseDto?>>()
      ?? new Response<TransactionResponseDto?>(null, 400, "Failure to update transaction");
  }
}
