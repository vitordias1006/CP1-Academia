using CP1_Academia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.Infrastructure.Persistence.Configurations;

public class LocalizacaoConfiguration : IEntityTypeConfiguration<Localizacao>
{
    public void Configure(EntityTypeBuilder<Localizacao> builder)
    {
        builder.ToTable("Localizacao");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Estado);
        builder.Property(x => x.Cidade);
        builder.Property(x => x.Bairro);
        builder.Property(x => x.Cep);
        builder.Property(x => x.Rua);
        builder.Property(x => x.Numero);
        
        builder.HasOne(x => x.UnidadeAcademia)
            .WithOne(u => u.Localizacao)  
            .HasForeignKey<Localizacao>(uc => uc.UnidadeAcademiaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}