using DotNetEnv;
using Scalar.AspNetCore;
using VideogameTracker.Application;
using VideogameTracker.Infrastructure; 

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresConnection")
    ?? throw new InvalidOperationException("Falta la cadena de conexión.");

builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString);


builder.Services.AddControllers(); 
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers(); 

app.Run();