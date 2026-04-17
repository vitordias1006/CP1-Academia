using CP1_Academia.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.API.Infrastructure.Persistence.Configurations;

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
        
        builder.HasOne(c => c.Plano)
            .WithMany(p => p.Alunos)
            .HasForeignKey(c => c.PlanoId)
            .OnDelete(DeleteBehavior.Cascade);
 
      
    }
    
}