using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.Infraestructure.Persistence.Configurations;

public class InstrutorConfiguration : IEntityTypeConfiguration<Instrutor>
{
    public void Configure(EntityTypeBuilder<Instrutor> builder)
    {
        builder.ToTable("Instrutor");
        
        builder.Property(i => i.Nome).IsRequired().HasMaxLength(60);
        
        builder.Property(i => i.Email).IsRequired().HasMaxLength(100);

        builder.Property(i => i.Cpf).IsRequired().HasMaxLength(14);
        
        builder.Property(i => i.Cargo).HasMaxLength(50);

        builder.Property(i => i.Salario).HasMaxLength(5);
        
        builder.Property(i => i.DataDeContratacao);
        
        builder.Property(i => i.Ativo);

        builder.Property(i => i.Cref).HasMaxLength(18);
    }
}