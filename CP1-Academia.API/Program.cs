using CP1_Academia.API.Application.Services;
using CP1_Academia.API.Exceptions;
using CP1_Academia.Infrastructure.Persistence;
using CP1_Academia.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Reflection;

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

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "CP1-Academia API",
                Version = "v1",
                Description = "API REST para gestão de uma rede de academias: " +
                              "alunos, planos, instrutores, funcionários, unidades e fichas de treino."
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });

        var app = builder.Build();

        app.UseExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CP1-Academia API v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}