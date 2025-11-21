using Get.Directorio.Infrastructure.Data;
using Get.Directorio.Infrastructure.Repositories;
using Get.Directorio.Core.Interfaces;
using Get.Directorio.Core.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

// DbContext
builder.Services.AddDbContext<DirectorioDbContext>(options =>
    options.UseSqlite("Data Source=directorio.db"));

builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<VentasService>();
builder.Services.AddScoped<DirectorioService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.MapControllers();
app.Run();

// Necesario para EF (si usas Minimal Hosting)
public partial class Program { }
