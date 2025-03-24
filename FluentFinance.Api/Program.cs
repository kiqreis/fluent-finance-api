using FluentFinance.Api.Data;
using FluentFinance.Core.Enums;
using FluentFinance.Core.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(opt =>
{
  opt.UseSqlServer(connectionString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Handler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/v1/transactions", (Request request, Handler handler) => handler.Handle(request))
  .WithName("Transactions: Create")
  .WithSummary("Create a new transaction")
  .Produces<Response<Transaction>>();

app.Run();

//Request

class Request
{
  public string Title { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public TransactionType Type { get; set; }
  public decimal Amount { get; set; }
  public long CategoryId { get; set; }
  public string UserId { get; set; } = string.Empty;
}

//Response

class Response<T>
{
  public T? Data { get; set; }
  public string Message { get; set; } = string.Empty;
}

//Handler

class Handler
{
  public Response<Transaction> Handle(Request request)
  {
    return new Response<Transaction>
    {
      Data = new Transaction
      {
        Id = 1,
        Title = request.Title,
        Type = request.Type,
        Amount = request.Amount,
        CategoryId = request.CategoryId,
        UserId = request.UserId
      },
      Message = "User created successfully"
    };
  } 
}