using FluentFinance.Api.Data;
using FluentFinance.Api.Handlers;
using FluentFinance.Api.Mappings;
using FluentFinance.Api.Models;
using FluentFinance.Core;
using FluentFinance.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FluentFinance.Api.Common.Api;

public static class BuilderExtension
{
  public static void AddConfiguration(this WebApplicationBuilder builder)
  {
    Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    Configuration.BackendUrl = builder.Configuration.GetValue<string>("Backendurl") ?? string.Empty;
  }

  public static void AddDocs(this WebApplicationBuilder builder)
  {
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
  }

  public static void AddSecurity(this WebApplicationBuilder builder)
  {
    builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
      .AddIdentityCookies();

    builder.Services.AddAuthorization();
  }

  public static void AddDataContexts(this WebApplicationBuilder builder)
  {
    builder.Services.AddDbContext<AppDbContext>(opt => { opt.UseSqlServer(Configuration.ConnectionString); });

    builder.Services.AddIdentityCore<User>()
      .AddRoles<IdentityRole<long>>()
      .AddEntityFrameworkStores<AppDbContext>()
      .AddApiEndpoints();
  }

  public static void AddCrossOrigins(this WebApplicationBuilder builder, IConfiguration configuration)
  {
    builder.Services.AddCors(opt => opt.AddPolicy(ApiConfiguration.CorsPolicyName,
      policy =>
      {
        policy.WithOrigins([Configuration.BackendUrl, Configuration.FrontendUrl])
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
      }));
  }

  public static void AddServices(this WebApplicationBuilder builder)
  {
    builder.Services.AddScoped<ICategoryHandler, CategoryHandler>();
    builder.Services.AddScoped<ITransactionHandler, TransactionHandler>();
  }

  public static void AddMapping(this WebApplicationBuilder builder)
  {
    builder.Services.AddAutoMapper(typeof(MappingProfile));
  }
}