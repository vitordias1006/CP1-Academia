using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;

namespace CP1_Academia.Infraestructure.Persistence;

public class AcademiaContext (DbContextOptions<AcademiaContext> options) : DbContext(options)
{
    
    public DbSet<Aluno>  Alunos { get; set; }
    
    public DbSet<AulaExtra> AulaExtras { get; set; }
    
    public DbSet<FichaTreino> FichaTreinos { get; set; }
    
    public DbSet<Funcionario> Funcionarios { get; set; }
    
    public DbSet<Gerente> Gerentes { get; set; }

    public DbSet<Instrutor> Instrutors { get; set; }

    public DbSet<Localizacao> Localizacoes { get; set; }

    public DbSet<Plano> Planos { get; set; }
    
    public DbSet<RedeAcademia> RedeAcademias { get; set; }
    
    public DbSet<UnidadeAcademia> UnidadeAcademias { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcademiaContext).Assembly);
    }
}