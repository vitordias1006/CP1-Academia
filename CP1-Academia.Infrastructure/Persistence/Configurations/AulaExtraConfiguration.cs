using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.Infraestructure.Persistence.Configurations;

public class AulaExtraConfiguration : IEntityTypeConfiguration<AulaExtra>
{

    public void Configure(EntityTypeBuilder<AulaExtra> builder)
    {
        builder.ToTable("AulaExtras");
        
        builder.HasKey(a => a.Id);

        builder.Property(a => a.TipoDeAula);
        builder.Property(a => a.HorarioAula);
        builder.Property(a => a.Capacidade);
        
       
    }
}