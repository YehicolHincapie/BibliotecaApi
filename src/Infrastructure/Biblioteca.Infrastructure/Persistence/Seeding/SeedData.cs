using Biblioteca.Domain.Entities;
using Biblioteca.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Persistence.Seeding;

public static class SeedData
{
    public static async Task InitializeAsync(BibliotecaDbContext context)
    {
        // Asegurar que la base de datos esté creada (para InMemory o desarrollo)
        await context.Database.EnsureCreatedAsync();

        if (await context.Libros.AnyAsync())
        {
            return; // La base de datos ya contiene datos
        }

        var autorGabo = new Autor(
            nombre: "Gabriel",
            apellido: "García Márquez",
            nacionalidad: "Colombiana",
            biografia: "Premio Nobel de Literatura 1982, maestro del realismo mágico y autor de obras cumbres de las letras hispanoamericanas."
        );

        var autorBorges = new Autor(
            nombre: "Jorge Luis",
            apellido: "Borges",
            nacionalidad: "Argentina",
            biografia: "Figura fundamental de la literatura universal, célebre por sus cuentos fantásticos, ensayos y poesía."
        );

        var autorAllende = new Autor(
            nombre: "Isabel",
            apellido: "Allende",
            nacionalidad: "Chilena",
            biografia: "Reconocida escritora contemporánea de superventas y miembro de la Academia Estadounidense de las Artes y las Letras."
        );

        var autorCervantes = new Autor(
            nombre: "Miguel",
            apellido: "de Cervantes Saavedra",
            nacionalidad: "Española",
            biografia: "Máxima figura de la literatura española y autor de una de las mejores obras de la literatura universal."
        );

        await context.Autores.AddRangeAsync(autorGabo, autorBorges, autorAllende, autorCervantes);
        await context.SaveChangesAsync();

        var catNovela = new Categoria(
            nombre: "Novela y Ficción",
            descripcion: "Obras narrativas de ficción literaria, realismo mágico y narrativa contemporánea."
        );

        var catClasicos = new Categoria(
            nombre: "Clásicos Universales",
            descripcion: "Grandes obras maestras de la literatura clásica mundial y del Siglo de Oro."
        );

        var catCuentos = new Categoria(
            nombre: "Cuentos y Relatos",
            descripcion: "Colecciones de relatos breves, laberintos literarios y ficción especulativa."
        );

        await context.Categorias.AddRangeAsync(catNovela, catClasicos, catCuentos);
        await context.SaveChangesAsync();

        var libros = new List<Libro>
        {
            new(
                titulo: "Cien años de soledad",
                isbn: "978-0307474728",
                anioPublicacion: 1967,
                autorId: autorGabo.Id,
                categoriaId: catNovela.Id
            ),
            new(
                titulo: "El amor en los tiempos del cólera",
                isbn: "978-0307389732",
                anioPublicacion: 1985,
                autorId: autorGabo.Id,
                categoriaId: catNovela.Id
            ),
            new(
                titulo: "Ficciones",
                isbn: "978-0307950925",
                anioPublicacion: 1944,
                autorId: autorBorges.Id,
                categoriaId: catCuentos.Id
            ),
            new(
                titulo: "El Aleph",
                isbn: "978-8420658797",
                anioPublicacion: 1949,
                autorId: autorBorges.Id,
                categoriaId: catCuentos.Id
            ),
            new(
                titulo: "La casa de los espíritus",
                isbn: "978-1501117015",
                anioPublicacion: 1982,
                autorId: autorAllende.Id,
                categoriaId: catNovela.Id
            ),
            new(
                titulo: "Don Quijote de la Mancha",
                isbn: "978-8420412146",
                anioPublicacion: 1605,
                autorId: autorCervantes.Id,
                categoriaId: catClasicos.Id
            )
        };

        await context.Libros.AddRangeAsync(libros);
        await context.SaveChangesAsync();
    }
}
