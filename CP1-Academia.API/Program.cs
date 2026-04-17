using CP1_Academia.Infraestructure;
using CP1_Academia.Infraestructure.Persistence;
using CP1_Application.Services;
using Microsoft.EntityFrameworkCore;
namespace CP1_Academia;
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

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}