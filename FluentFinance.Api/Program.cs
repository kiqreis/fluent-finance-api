using FluentFinance.Api.Common.Api;
using FluentFinance.Api.Data;
using FluentFinance.Api.Handlers;
using FluentFinance.Api.Mappings;
using FluentFinance.Api.Models;
using FluentFinance.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
  .AddIdentityCookies();
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(opt =>
{
  opt.UseSqlServer(connectionString);
});

builder.Services.AddIdentityCore<User>()
  .AddRoles<IdentityRole<long>>()
  .AddEntityFrameworkStores<AppDbContext>()
  .AddApiEndpoints(); 

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ICategoryHandler, CategoryHandler>();
builder.Services.AddScoped<ITransactionHandler, TransactionHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.Run();
