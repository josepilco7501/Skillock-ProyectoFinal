using Microsoft.OpenApi.Models;
using Lab10.Infrastructure2.Configuration; // Tu namespace de infraestructura

namespace Lab10.Configuration;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Habilitar HttpContextAccessor
        services.AddHttpContextAccessor();

// 2. Registro de servicios de la capa de Infraestructura
        services.AddInfrastructureServices(configuration);

// 4. Habilitar controladores del API
        services.AddControllers();

// 5. Configuración y habilitación de Swagger con el botón de autorización (Candado)
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Lab10 API",
                Version = "v1",
                Description = "API para gestionar recursos con Arquitectura Limpia."
            });

            // Configuración del botón "Authorize" y el Candado
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Ingresa el token JWT de esta manera: Bearer {tu_token_aqui}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });


        return services;

    }
}
