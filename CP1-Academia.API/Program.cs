using CP1_Academia.API.Application.Services;
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
        
        
        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

        // Configure the HTTP request pipeline.
       builder.Services.AddEndpointsApiExplorer();
       builder.Services.AddSwaggerGen();
       
        var app = builder.Build();

       
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