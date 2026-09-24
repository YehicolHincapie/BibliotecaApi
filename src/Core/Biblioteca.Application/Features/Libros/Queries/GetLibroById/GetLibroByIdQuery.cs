using Biblioteca.Application.Dtos;
using MediatR;

namespace Biblioteca.Application.Features.Libros.Queries.GetLibroById;

public record GetLibroByIdQuery(int Id) : IRequest<LibroDetalleDto?>;
