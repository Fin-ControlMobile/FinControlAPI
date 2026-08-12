using FinControlAPI.Applications.Services;
using FinControlAPI.Contexts;
using FinControlAPI.Interfaces;
using FinControlAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// CARREGA O .env
DotNetEnv.Env.Load();

// PEGA A CONNECTION STRING DO .env
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
builder.Services.AddSwaggerGen();

// CONECTA AO BANCO COM A CONNECTION STRING DO .env
builder.Services.AddDbContext<FinControlDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

// USUÁRIO
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

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
