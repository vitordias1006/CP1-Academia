using Asp.Versioning;                                    // CP5
using Asp.Versioning.ApiExplorer;                        // CP5
using CP1_Academia.API.Application.Services;
using CP1_Academia.API.Exceptions;
using CP1_Academia.API.HealthChecks;
using CP1_Academia.API.RateLimiting;                     // CP5
using CP1_Academia.API.Swagger;                          // CP5
using CP1_Academia.Infrastructure;
using CP1_Academia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;                      // CP5
using Swashbuckle.AspNetCore.SwaggerGen;                 // CP5
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

        // ===================== — Versionamento =====================
        builder.Services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;   // sem versão => 2.0
                options.ReportApiVersions = true;                     // api-supported-versions / api-deprecated-versions
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),   // ?api-version=1.0
                    new HeaderApiVersionReader("X-Api-Version"),      // X-Api-Version: 1.0
                    new UrlSegmentApiVersionReader());                // /api/v1/Aluno  (recomendado)
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVVV";                  // v1.0, v2.0
                options.SubstituteApiVersionInUrl = true;
            });

        // ===================== — Rate limit =====================
        builder.Services.AddAcademiaRateLimiting();

        // ===================== Swagger (um documento por versão) =====================
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>(); 
        builder.Services.AddSwaggerGen(options =>
        {
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
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var d in provider.ApiVersionDescriptions.OrderByDescending(d => d.ApiVersion))
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{d.GroupName}/swagger.json",
                        d.IsDeprecated ? $"{d.GroupName} (DEPRECADA)" : d.GroupName);
                }
            });
        }

        app.UseHttpsRedirection();

        app.UseRouting();                                              

        app.UseRateLimiter();                                          

        app.UseAuthorization();

        app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
        {
            ResponseWriter = HealthCheckWriter.WriteResponse
        }).DisableRateLimiting();                                      

        app.MapControllers();

        app.Run();
    }
}