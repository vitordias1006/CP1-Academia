using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.Infraestructure.Persistence.Configurations;

public class UnidadeAcademiaConfiguration : IEntityTypeConfiguration<UnidadeAcademia>
{

    public void Configure(EntityTypeBuilder<UnidadeAcademia> builder)
    {
        builder.ToTable("UnidadeAcademia");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Telefone);
        
        builder.Property(u => u.Ativo);
        
        builder.Property(u => u.HorarioFuncionamento);
        
        builder.HasOne(u => u.RedeAcademia).WithOne().HasForeignKey<ClassLibrary1.Entities.UnidadeAcademia>(uc => uc.RedeAcademiaId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(u => u.Gerente).WithOne().HasForeignKey<ClassLibrary1.Entities.UnidadeAcademia>(uc => uc.GerenteId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(u => u.Localizacao).WithOne().HasForeignKey<ClassLibrary1.Entities.UnidadeAcademia>(uc => uc.Localizacao)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(u => u.Funcionarios)
            .WithOne()
            .HasForeignKey(f => f.UnidadeAcademiaId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}