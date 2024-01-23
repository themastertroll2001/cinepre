using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAppi_Telefono.Models;
var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la cadena de conexi�n desde appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");
var connectionString = builder.Configuration.GetConnectionString("BdCineConnection");

// Agregar servicios a la colecci�n.
builder.Services.AddDbContext<BdCineContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
