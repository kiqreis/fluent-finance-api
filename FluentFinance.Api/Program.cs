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

app.MapPost("/v1/categories", (Request request, Handler handler) => handler.Handle(request))
  .WithName("Categories: Create")
  .WithSummary("Create a new category")
  .Produces<Response<Transaction>>();

app.Run();

//Request

class Request
{
  public long Id { get; set; }
  public string Title { get; set; } = string.Empty;
}

//Response

class Response<T>
{
  public T? Data { get; set; }
  public string Message { get; set; } = string.Empty;
}

//Handler

class Handler(AppDbContext context)
{
  public Response<Category> Handle(Request request)
  {
    var category = new Category
    {
      Id = request.Id,
      Title = request.Title
    };

    context.Categories.Add(category);
    context.SaveChanges();

    return new Response<Category>
    {
      Data = category,
      Message = "Category created successfully"
    };
  }
}