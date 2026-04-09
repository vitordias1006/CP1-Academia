using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.Infraestructure.Persistence.Configurations;

public class RedeAcademiaConfiguration : IEntityTypeConfiguration<RedeAcademia>
{
    public void Configure(EntityTypeBuilder<RedeAcademia> builder)
    {
        builder.ToTable("RedeAcademia");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome);
        builder.Property(x => x.QntdUnidades);
        builder.Property(x => x.Cnpj);
        builder.Property(x => x.DataFundacao);
        
        builder.HasMany(x => x.UnidadesAcademia)
            .WithOne()
            .HasForeignKey(u => u.RedeAcademiaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}