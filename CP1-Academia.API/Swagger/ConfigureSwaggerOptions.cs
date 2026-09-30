using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CP1_Academia.API.Swagger;

/// <summary>Cria um documento Swagger para cada versão descoberta pelo ApiExplorer.</summary>
public sealed class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            var info = new OpenApiInfo
            {
                Title = "CP1-Academia API",
                Version = description.ApiVersion.ToString(),
                Description = "API REST para gestão de uma rede de academias."
            };

            if (description.IsDeprecated)
            {
                info.Description +=
                    " ⚠️ **ESTA VERSÃO ESTÁ DEPRECADA.** Ela continua funcionando, mas será removida no futuro. " +
                    "Migre para a v2.0, cuja listagem de alunos é paginada.";
            }

            options.SwaggerDoc(description.GroupName, info);
        }
    }
}