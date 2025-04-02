using FluentFinance.Api.Common.Api;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigins();
builder.AddDocs();
builder.AddMapping();
builder.AddServices();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.Run();
