namespace Biblioteca.Application.Dtos;

public record AutorDto(
    int Id,
    string Nombre,
    string Apellido,
    string NombreCompleto,
    string Nacionalidad,
    string Biografia
);
