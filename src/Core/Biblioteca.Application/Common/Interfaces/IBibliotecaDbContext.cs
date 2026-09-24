using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Application.Common.Interfaces;

public interface IBibliotecaDbContext
{
    DbSet<Libro> Libros { get; }
    DbSet<Autor> Autores { get; }
    DbSet<Categoria> Categorias { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
