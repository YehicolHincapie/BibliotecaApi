namespace Biblioteca.Application.Dtos;

public record LibroDetalleDto(
    int Id,
    string Titulo,
    string Isbn,
    int AnioPublicacion,
    AutorDto Autor,
    CategoriaDto Categoria
);
