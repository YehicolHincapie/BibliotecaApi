using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Infrastructure.Persistence.Configurations;

public class AutorConfiguration : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.ToTable("Autores");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Apellido)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Nacionalidad)
            .HasMaxLength(100);

        builder.Property(a => a.Biografia)
            .HasMaxLength(1000);

        builder.Ignore(a => a.NombreCompleto);

        builder.HasMany(a => a.Libros)
            .WithOne(l => l.Autor)
            .HasForeignKey(l => l.AutorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
