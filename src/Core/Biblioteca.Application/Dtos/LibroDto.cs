namespace Biblioteca.Application.Dtos;

public record LibroDto(
    int Id,
    string Titulo,
    string Isbn,
    int AnioPublicacion,
    string Autor,
    string Categoria
);
