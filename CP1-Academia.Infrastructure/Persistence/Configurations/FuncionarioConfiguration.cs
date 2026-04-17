using CP1_Academia.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.API.Infrastructure.Persistence.Configurations;

public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable("Funcionarios");
        
        builder.HasKey(f => f.Id);
        
        builder.Property(f => f.Nome).IsRequired().HasMaxLength(60);
        
        builder.Property(f => f.Email).IsRequired().HasMaxLength(100);

        builder.Property(f => f.Cpf).IsRequired().HasMaxLength(14);
        
        builder.Property(f => f.Cargo).HasMaxLength(50);

        builder.Property(f => f.Salario).HasMaxLength(5);
        
        builder.Property(f => f.DataDeContratacao);
        
        builder.Property(f => f.Ativo);
    }
}