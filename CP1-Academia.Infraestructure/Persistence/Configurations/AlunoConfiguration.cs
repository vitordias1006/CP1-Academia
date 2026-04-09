using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.Infraestructure.Persistence.Configurations;

public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{

    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("Alunos");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Nome).IsRequired().HasMaxLength(60);
        
        builder.Property(c => c.Email).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Cpf).IsRequired().HasMaxLength(14);
        
        builder.Property(c =>  c.Telefone).IsRequired().HasMaxLength(11);
        
        builder.Property(c => c.DataMatricula).IsRequired();
        
        builder.Property(c => c.Ativo).IsRequired();

        builder.HasOne(c => c.Plano).WithOne().HasForeignKey<ClassLibrary1.Entities.Aluno>(uc => uc.PlanoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(c => c.FichaTreino).WithOne().HasForeignKey<ClassLibrary1.Entities.Aluno>(uc => uc.FichaTreinoId)
            .OnDelete(DeleteBehavior.Cascade);

    }
    
}