using CP1_Academia.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.API.Infrastructure.Persistence.Configurations;

public class GerenteConfiguration : IEntityTypeConfiguration<Gerente>
{

    public void Configure(EntityTypeBuilder<Gerente> builder)
    {
        builder.ToTable("Gerentes");
        
        builder.Property(f => f.Nome).IsRequired().HasMaxLength(60);
        
        builder.Property(f => f.Email).IsRequired().HasMaxLength(100);

        builder.Property(f => f.Cpf).IsRequired().HasMaxLength(14);
        
        builder.Property(f => f.Cargo).HasMaxLength(50);

        builder.Property(f => f.Salario).HasMaxLength(5);
        
        builder.Property(f => f.DataDeContratacao);
        
        builder.Property(f => f.Ativo);
        
        builder.Property(g => g.Comissao).HasMaxLength(5);
        
        builder.Property(g => g.PeriodoDeLideranca);
        
        builder.Property(g => g.AreaDeResponsabilidade).HasMaxLength(50);
        
        builder.Property(g => g.NivelDeLideranca).HasMaxLength(6);
        
        builder.HasMany(g => g.Funcionarios)
            .WithOne(f => f.Gerente)
            .HasForeignKey(f => f.GerenteId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(g => g.UnidadesAcademia)
            .WithOne(u => u.Gerente)
            .HasForeignKey(u => u.GerenteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}