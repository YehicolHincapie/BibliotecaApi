using Biblioteca.Application.Common.Interfaces;
using Biblioteca.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Application.Features.Libros.Queries.GetAllLibros;

public class GetAllLibrosQueryHandler : IRequestHandler<GetAllLibrosQuery, IReadOnlyList<LibroDto>>
{
    private readonly IBibliotecaDbContext _context;

    public GetAllLibrosQueryHandler(IBibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<LibroDto>> Handle(GetAllLibrosQuery request, CancellationToken cancellationToken)
    {
        var libros = await _context.Libros
            .AsNoTracking()
            .Include(l => l.Autor)
            .Include(l => l.Categoria)
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
