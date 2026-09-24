using Biblioteca.Domain.Common;

namespace Biblioteca.Domain.Entities;

public class Autor : BaseEntity
{
    private readonly List<Libro> _libros = new();

    public string Nombre { get; private set; } = string.Empty;
    public string Apellido { get; private set; } = string.Empty;
    public string Nacionalidad { get; private set; } = string.Empty;
    public string Biografia { get; private set; } = string.Empty;

    public string NombreCompleto => $"{Nombre} {Apellido}".Trim();

    public IReadOnlyCollection<Libro> Libros => _libros.AsReadOnly();

    // Constructor protegido para EF Core
    protected Autor() { }

    public Autor(string nombre, string apellido, string nacionalidad, string biografia)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nombre);
        ArgumentException.ThrowIfNullOrWhiteSpace(apellido);

        Nombre = nombre.Trim();
        Apellido = apellido.Trim();
        Nacionalidad = (nacionalidad ?? string.Empty).Trim();
        Biografia = (biografia ?? string.Empty).Trim();
    }

    public Autor(int id, string nombre, string apellido, string nacionalidad, string biografia)
        : this(nombre, apellido, nacionalidad, biografia)
    {
        Id = id;
    }
}
