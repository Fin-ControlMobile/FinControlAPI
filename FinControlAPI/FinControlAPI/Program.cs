using DotNetEnv;
using FinControlAPI.Aplication.Service;
using FinControlAPI.Contexts;
using FinControlAPI.Interface;
using FinControlAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// PEGA A CONNECTION STRING DO .env
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

// CONECTA AO BANCO COM A CONNECTION STRING DO .env
builder.Services.AddDbContext<FinControlDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<TransacaoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
