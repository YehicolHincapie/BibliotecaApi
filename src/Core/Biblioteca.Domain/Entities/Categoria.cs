using Biblioteca.Domain.Common;

namespace Biblioteca.Domain.Entities;

public class Categoria : BaseEntity
{
    private readonly List<Libro> _libros = new();

    public string Nombre { get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;

    public IReadOnlyCollection<Libro> Libros => _libros.AsReadOnly();

    // Constructor protegido para EF Core
    protected Categoria() { }

    public Categoria(string nombre, string descripcion)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nombre);

        Nombre = nombre.Trim();
        Descripcion = (descripcion ?? string.Empty).Trim();
    }

    public Categoria(int id, string nombre, string descripcion)
        : this(nombre, descripcion)
    {
        Id = id;
    }
}
