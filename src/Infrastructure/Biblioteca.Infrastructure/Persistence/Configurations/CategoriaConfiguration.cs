using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Persistence.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Descripcion)
            .HasMaxLength(500);

        builder.HasMany(c => c.Libros)
            .WithOne(l => l.Categoria)
            .HasForeignKey(l => l.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
