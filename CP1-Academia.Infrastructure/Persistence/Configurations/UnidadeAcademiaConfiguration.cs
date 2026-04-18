using CP1_Academia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.Infrastructure.Persistence.Configurations;

public class UnidadeAcademiaConfiguration : IEntityTypeConfiguration<UnidadeAcademia>
{

    public void Configure(EntityTypeBuilder<UnidadeAcademia> builder)
    {
        builder.ToTable("UnidadeAcademia");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Telefone);
        
        builder.Property(u => u.Ativo);
        
        builder.Property(u => u.HorarioFuncionamento);
        
        builder.HasMany(u => u.Funcionarios)
            .WithOne(f => f.UnidadeAcademia)
            .HasForeignKey(f => f.UnidadeAcademiaId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}