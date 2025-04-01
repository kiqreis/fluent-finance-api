using AutoMapper;
using FluentFinance.Api.Data;
using FluentFinance.Core.Common.Extensions;
using FluentFinance.Core.Handlers;
using FluentFinance.Core.Models;
using FluentFinance.Core.Requests.Transactions;
using FluentFinance.Core.Responses;
using FluentFinance.Core.Responses.Transactions;
using Microsoft.EntityFrameworkCore;

namespace FluentFinance.Api.Handlers;

public class TransactionHandler(AppDbContext context, IMapper mapper) : ITransactionHandler
{
  public async Task<Response<TransactionResponseDto>> CreateAsync(CreateTransactionRequest request)
  {
    var transaction = new Transaction
    {
      UserId = request.UserId,
      Title = request.Title,
      Type = request.Type,
      Amount = request.Amount,
      CategoryId = request.CategoryId,
      PaidOrReceivedAt = request.PaidOrReceivedAt
    };

    await context.Transactions.AddAsync(transaction);
    await context.SaveChangesAsync();

    var response = mapper.Map<TransactionResponseDto>(transaction);

    return new Response<TransactionResponseDto>(response, 201, "Transaction created successfully");
  }

  public async Task<Response<TransactionResponseDto?>> UpdateAsync(UpdateTransactionRequest request)
  {
    var transaction = await context.Transactions.FirstOrDefaultAsync(r => r.UserId == request.UserId);

    if (transaction is null)
      return new Response<TransactionResponseDto?>(null, 404, "Transaction not found");

    transaction.Title = request.Title;
    transaction.Type = request.Type;
    transaction.Amount = request.Amount;
    transaction.CategoryId = request.CategoryId;
    transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

    context.Transactions.Update(transaction);
    await context.SaveChangesAsync();

    var response = mapper.Map<TransactionResponseDto>(transaction);

    return new Response<TransactionResponseDto?>(response, message: "Transaction updated successfully");
  }

  public async Task<Response<TransactionResponseDto?>> DeleteAsync(long id)
  {
    var transaction = await context.Transactions.FindAsync(id);

    if (transaction is null)
      return new Response<TransactionResponseDto?>(null, 404, "Transaction not found");

    context.Transactions.Remove(transaction);
    await context.SaveChangesAsync();

    var result = mapper.Map<TransactionResponseDto>(transaction);

    return new Response<TransactionResponseDto?>(result, message: "Transaction deleted successfully");
  }

  public async Task<Response<TransactionResponseDto?>> GetById(long id)
  {
    var transaction = await context.Transactions.FindAsync(id);

    if (transaction is null)
      return new Response<TransactionResponseDto?>(null, 404, "Transaction not found");

    var response = mapper.Map<TransactionResponseDto?>(transaction);

    return new Response<TransactionResponseDto?>(response, message: "Transaction found successfully");
  }

  public async Task<PagedResponse<IList<TransactionResponseDto>>> GetByPeriod(GetTransactionByPeriodRequest request)
  {
    request.StartDate ??= DateTime.Now.GetFirstDay();
    request.EndDate ??= DateTime.Now.GetLastDay();

    var query = context.Transactions.AsNoTracking()
      .Where(t => t.CreatedAt >= request.StartDate && t.CreatedAt <= request.EndDate)
      .OrderByDescending(t => t.CreatedAt);
    
    var transactions = await query.Skip((request.PageNumber - 1) * request.PageSize)
      .Take(request.PageSize)
      .ToListAsync();

    var count = await query.CountAsync();

    var response = mapper.Map<IList<TransactionResponseDto>>(transactions);

    return new PagedResponse<IList<TransactionResponseDto>>(response, count, request.PageNumber, request.PageSize);
  }
}