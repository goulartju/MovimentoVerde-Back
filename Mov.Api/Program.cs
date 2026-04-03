using Mov.Api;
using System.Reflection;
using Microsoft.Extensions.Configuration.UserSecrets;

var builder = WebApplication.CreateBuilder(args);
// Load user secrets (optional) so connection strings and other secrets stored
// with `dotnet user-secrets` are available in Configuration during development.
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAppDI(builder.Configuration);


var app = builder.Build();

//app.UseSwagger();
//app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();