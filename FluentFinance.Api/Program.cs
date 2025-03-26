using FluentFinance.Api.Data;
using FluentFinance.Api.Handlers;
using FluentFinance.Api.Mappings;
using FluentFinance.Api.Routes;
using FluentFinance.Core.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(opt =>
{
  opt.UseSqlServer(connectionString);
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ICategoryHandler, CategoryHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapCategoryRoutes();

app.Run();
