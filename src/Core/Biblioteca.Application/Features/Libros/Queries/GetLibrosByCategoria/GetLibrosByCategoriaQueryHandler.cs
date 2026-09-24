using Biblioteca.Application.Common.Interfaces;
using Biblioteca.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Application.Features.Libros.Queries.GetLibrosByCategoria;

public class GetLibrosByCategoriaQueryHandler : IRequestHandler<GetLibrosByCategoriaQuery, IReadOnlyList<LibroDto>>
{
    private readonly IBibliotecaDbContext _context;

    public GetLibrosByCategoriaQueryHandler(IBibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<LibroDto>> Handle(GetLibrosByCategoriaQuery request, CancellationToken cancellationToken)
    {
        var libros = await _context.Libros
            .AsNoTracking()
            .Where(l => l.CategoriaId == request.CategoriaId)
            .Include(l => l.Autor)
            .Include(l => l.Categoria)
            .OrderBy(l => l.Titulo)
            .Select(l => new LibroDto(
                l.Id,
                l.Titulo,
                l.Isbn,
                l.AnioPublicacion,
                l.Autor != null ? l.Autor.NombreCompleto : string.Empty,
                l.Categoria != null ? l.Categoria.Nombre : string.Empty
            ))
            .ToListAsync(cancellationToken);

        return libros;
    }
}
