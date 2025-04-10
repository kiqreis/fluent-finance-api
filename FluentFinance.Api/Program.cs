using FluentFinance.Api;
using FluentFinance.Api.Common.Api;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigins(builder.Configuration);
builder.AddDocs();
builder.AddMapping();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.ConfigureDevEnvironment();
}

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();

app.Run();
  