using Biblioteca.Application.Dtos;
using MediatR;

namespace Biblioteca.Application.Features.Libros.Queries.GetLibrosByCategoria;

public record GetLibrosByCategoriaQuery(int CategoriaId) : IRequest<IReadOnlyList<LibroDto>>;
