using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Api.Data;
using PruebaTecnica.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registrar DbContext y Service (SIN DUPLICAR)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ProductosService>(); // ✅ Solo una vez

// Swagger (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS para permitir que Blazor UI consuma la API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorUI", policy =>
    {
        policy.WithOrigins("http://localhost:5200", "https://localhost:5201") // Puertos de Blazor UI
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Swagger siempre habilitado (para desarrollo y pruebas)
app.UseSwagger();
app.UseSwaggerUI();

// Habilitar CORS
app.UseCors("AllowBlazorUI");

// Redirección de la raíz a Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// Mapear controladores
app.MapControllers();

// Run debe ir AL FINAL
app.Run();