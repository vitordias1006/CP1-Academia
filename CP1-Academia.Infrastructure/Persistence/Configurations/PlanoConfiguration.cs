using CP1_Academia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.Infrastructure.Persistence.Configurations;

public class PlanoConfiguration : IEntityTypeConfiguration<Plano>
{
    public void Configure(EntityTypeBuilder<Plano> builder)
    {
        builder.ToTable("Planos");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Preco);

        builder.Property(p => p.DataDeAssinatura);
        
        builder.Property(p => p.DataDeRenovacao);
        
        builder.Property(p => p.TipoPlano).IsRequired().HasMaxLength(40);

        builder.Property(p => p.Fidelidade);

        builder.Property(p => p.Ativo);
        
    }
}