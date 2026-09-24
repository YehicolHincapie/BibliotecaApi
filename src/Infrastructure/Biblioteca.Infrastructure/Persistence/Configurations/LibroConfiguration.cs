using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Persistence.Configurations;

public class LibroConfiguration : IEntityTypeConfiguration<Libro>
{
    public void Configure(EntityTypeBuilder<Libro> builder)
    {
        builder.ToTable("Libros");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(l => l.Isbn)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(l => l.AnioPublicacion)
            .IsRequired();

        builder.HasIndex(l => l.Isbn)
            .IsUnique();

        builder.HasOne(l => l.Autor)
            .WithMany(a => a.Libros)
            .HasForeignKey(l => l.AutorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Categoria)
            .WithMany(c => c.Libros)
            .HasForeignKey(l => l.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
