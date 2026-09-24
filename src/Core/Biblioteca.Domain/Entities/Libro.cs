using Biblioteca.Domain.Common;

namespace Biblioteca.Domain.Entities;

public class Libro : BaseEntity
{
    public string Titulo { get; private set; } = string.Empty;
    public string Isbn { get; private set; } = string.Empty;
    public int AnioPublicacion { get; private set; }

    public int AutorId { get; private set; }
    public Autor Autor { get; private set; } = null!;

    public int CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; } = null!;

    // Constructor protegido para EF Core
    protected Libro() { }

    public Libro(string titulo, string isbn, int anioPublicacion, int autorId, int categoriaId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(titulo);
        ArgumentException.ThrowIfNullOrWhiteSpace(isbn);

        if (anioPublicacion <= 0 || anioPublicacion > 2100)
        {
            throw new ArgumentOutOfRangeException(nameof(anioPublicacion), "El año de publicación debe ser válido.");
        }

        if (autorId <= 0)
        {
            throw new ArgumentException("El identificador de autor debe ser mayor que cero.", nameof(autorId));
        }

        if (categoriaId <= 0)
        {
            throw new ArgumentException("El identificador de categoría debe ser mayor que cero.", nameof(categoriaId));
        }

        Titulo = titulo.Trim();
        Isbn = isbn.Trim();
        AnioPublicacion = anioPublicacion;
        AutorId = autorId;
        CategoriaId = categoriaId;
    }

    public Libro(int id, string titulo, string isbn, int anioPublicacion, int autorId, int categoriaId)
        : this(titulo, isbn, anioPublicacion, autorId, categoriaId)
    {
        Id = id;
    }
}
