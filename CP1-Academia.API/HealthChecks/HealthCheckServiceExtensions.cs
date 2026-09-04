using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.API.HealthChecks;

public static class HealthCheckServiceExtensions
{
    public static IServiceCollection AddAcademiaHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("self", () =>
                Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy("API no ar"))
            .AddDbContextCheck<AcademiaContext>("oracle-db");

        return services;
    }
}