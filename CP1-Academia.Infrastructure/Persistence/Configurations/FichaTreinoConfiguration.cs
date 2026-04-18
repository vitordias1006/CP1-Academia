using CP1_Academia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CP1_Academia.Infrastructure.Persistence.Configurations;

public class FichaTreinoConfiguration : IEntityTypeConfiguration<FichaTreino>
{

    public void Configure(EntityTypeBuilder<FichaTreino> builder)
    {
        builder.ToTable("FichaTreino");
        
         builder.HasKey(p => p.Id);

         builder.Property(f => f.Exercicios).HasMaxLength(80);
         
         builder.Property(f => f.Repeticoes).HasMaxLength(2);
         
         builder.Property(f => f.MusculoAlvo).HasMaxLength(60);
         
         builder.Property(f => f.Observacao).HasMaxLength(400);
         
         builder.Property(f => f.TipoExercicio).HasMaxLength(60);
         
         builder.Property(f => f.Series).HasMaxLength(2);
         
         
         builder.HasOne(f => f.Aluno)
             .WithOne(a => a.FichaTreino)
             .HasForeignKey<FichaTreino>(f => f.AlunoId)
             .OnDelete(DeleteBehavior.Cascade);
 
         builder.HasMany(f => f.AulaExtras)
             .WithMany(a => a.FichaTreinos)
             .UsingEntity(j => j.ToTable("FichaTreinoAulasExtras"));
    }
    
}