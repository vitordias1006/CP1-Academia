using CP1_Academia.API.Application.Services;
using CP1_Academia.API.Exceptions;
using CP1_Academia.API.HealthChecks;
using CP1_Academia.Infrastructure.Persistence;
using CP1_Academia.Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace CP1_Academia.API;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddDbContext<AcademiaContext>(options =>
        {
            options.UseOracle(builder.Configuration.GetConnectionString("AcademiaOracle"));
        });
        
        builder.Services.AddAcademiaHealthChecks();
        
        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
        builder.Services.AddScoped<IAulaExtraRepository, AulaExtraRepository>();
        builder.Services.AddScoped<IFichaTreinoRepository, FichaTreinoRepository>();
        builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        builder.Services.AddScoped<IGerenteRepository, GerenteRepository>();
        builder.Services.AddScoped<IInstrutorRepository, InstrutorRepository>();
        builder.Services.AddScoped<ILocalizacaoRespository, LocalizacaoRepository>();
        builder.Services.AddScoped<IPlanoRepository, PlanoRepository>();
        builder.Services.AddScoped<IRedeAcademiaRepository, RedeAcademiaRepository>();
        builder.Services.AddScoped<IUnidadeAcademiaRepository, UnidadeAcademiaRepository>();
        
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

        // Configure the HTTP request pipeline.
       builder.Services.AddEndpointsApiExplorer();
       builder.Services.AddSwaggerGen();
       
        var app = builder.Build();
    
        app.UseExceptionHandler();
       
       if (app.Environment.IsDevelopment())
       {
           app.UseSwagger();
           app.UseSwaggerUI();
       }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}