using Biblioteca.Application.Common.Interfaces;
using Biblioteca.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Application.Features.Libros.Queries.GetLibroById;

public class GetLibroByIdQueryHandler : IRequestHandler<GetLibroByIdQuery, LibroDetalleDto?>
{
    private readonly IBibliotecaDbContext _context;

    public GetLibroByIdQueryHandler(IBibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<LibroDetalleDto?> Handle(GetLibroByIdQuery request, CancellationToken cancellationToken)
    {
        var libro = await _context.Libros
            .AsNoTracking()
            .Include(l => l.Autor)
            .Include(l => l.Categoria)
            .Where(l => l.Id == request.Id)
            .Select(l => new LibroDetalleDto(
                l.Id,
                l.Titulo,
                l.Isbn,
                l.AnioPublicacion,
                new AutorDto(
                    l.Autor.Id,
                    l.Autor.Nombre,
                    l.Autor.Apellido,
                    l.Autor.NombreCompleto,
                    l.Autor.Nacionalidad,
                    l.Autor.Biografia
                ),
                new CategoriaDto(
                    l.Categoria.Id,
                    l.Categoria.Nombre,
                    l.Categoria.Descripcion
                )
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return libro;
    }
}
