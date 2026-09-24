using Biblioteca.Application.Common.Interfaces;
using Biblioteca.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var useInMemoryConfig = configuration["UseInMemoryDatabase"];
        var isInMemory = bool.TryParse(useInMemoryConfig, out var parsedVal) && parsedVal;
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (isInMemory || string.IsNullOrWhiteSpace(connectionString))
        {
            services.AddDbContext<BibliotecaDbContext>(options =>
                options.UseInMemoryDatabase("BibliotecaDb"));
        }
        else
        {
            services.AddDbContext<BibliotecaDbContext>(options =>
                options.UseSqlServer(connectionString, b =>
                    b.MigrationsAssembly(typeof(BibliotecaDbContext).Assembly.FullName)));
        }

        services.AddScoped<IBibliotecaDbContext>(provider =>
            provider.GetRequiredService<BibliotecaDbContext>());

        return services;
    }
}
