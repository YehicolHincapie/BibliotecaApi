using Biblioteca.Application.Dtos;
using MediatR;

namespace Biblioteca.Application.Features.Libros.Queries.GetAllLibros;

public record GetAllLibrosQuery : IRequest<IReadOnlyList<LibroDto>>;
