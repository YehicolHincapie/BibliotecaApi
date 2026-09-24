using Biblioteca.Application;
using Biblioteca.Infrastructure;
using Biblioteca.Infrastructure.Persistence.Contexts;
using Biblioteca.Infrastructure.Persistence.Seeding;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Registro de servicios por capas (Clean Architecture)
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// 2. Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Biblioteca API - Catálogo de Libros",
        Version = "v1",
        Description = "API REST en .NET 8 con Clean Architecture, CQRS (MediatR) y EF Core para consultas del catálogo de biblioteca.",
        Contact = new OpenApiContact
        {
            Name = "Equipo de Arquitectura de Software",
        }
    });
});

var app = builder.Build();

// 3. Inicialización y Seeding de la base de datos al arrancar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<BibliotecaDbContext>();
        await SeedData.InitializeAsync(dbContext);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al inicializar o poblar la base de datos.");
    }
}

// 4. Configuración del Pipeline HTTP
if (app.Environment.IsDevelopment() || true) // Permitir Swagger para visualización y pruebas
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biblioteca API v1");
        c.RoutePrefix = string.Empty; // Swagger UI en la raíz (http://localhost:port/)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
