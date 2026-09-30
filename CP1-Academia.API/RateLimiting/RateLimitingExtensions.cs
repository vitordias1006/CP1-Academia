using System.Globalization;
using System.Text.Json;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Mvc;

namespace CP1_Academia.API.RateLimiting;

public static class RateLimitPolicies
{
    /// <summary>Política aplicada ao POST /api/Aluno.</summary>
    public const string Escrita = "escrita";

    public const int EscritaPermitLimit = 10;
    public static readonly TimeSpan EscritaWindow = TimeSpan.FromMinutes(1);
}

public static class RateLimitingExtensions
{
    public static IServiceCollection AddAcademiaRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            // Fixed window: 10 requisições por minuto, particionado por IP
            options.AddPolicy(RateLimitPolicies.Escrita, httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "desconhecido",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = RateLimitPolicies.EscritaPermitLimit,
                        Window = RateLimitPolicies.EscritaWindow,
                        QueueLimit = 0,              // estourou => rejeita na hora
                        AutoReplenishment = true
                    }));

            options.OnRejected = async (context, cancellationToken) =>
            {
                var http = context.HttpContext;

                var retryAfterSeconds =
                    context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter)
                        ? (int)Math.Ceiling(retryAfter.TotalSeconds)
                        : (int)RateLimitPolicies.EscritaWindow.TotalSeconds;

                http.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                http.Response.Headers.RetryAfter = retryAfterSeconds.ToString(CultureInfo.InvariantCulture);

                var problem = new ProblemDetails
                {
                    Status = StatusCodes.Status429TooManyRequests,
                    Title = "Muitas requisições",
                    Detail = $"Limite de {RateLimitPolicies.EscritaPermitLimit} requisições por minuto excedido. " +
                             $"Tente novamente em {retryAfterSeconds} segundo(s).",
                    Type = "https://httpstatuses.com/429"
                };
                problem.Extensions["traceId"] = http.TraceIdentifier;
                problem.Extensions["retryAfterSeconds"] = retryAfterSeconds;

                await http.Response.WriteAsJsonAsync(
                    problem,
                    (JsonSerializerOptions?)null,
                    "application/problem+json",
                    cancellationToken);
            };
        });

        return services;
    }
}