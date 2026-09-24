using Biblioteca.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Biblioteca.Infrastructure.Persistence;

public class BibliotecaDbContextFactory : IDesignTimeDbContextFactory<BibliotecaDbContext>
{
    public BibliotecaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BibliotecaDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=BibliotecaDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
            b => b.MigrationsAssembly(typeof(BibliotecaDbContext).Assembly.FullName)
        );

        return new BibliotecaDbContext(optionsBuilder.Options);
    }
}
