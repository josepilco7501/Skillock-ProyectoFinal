using System.Reflection;
using System.Text;
using Lab10.Application2.Interfaces;
using Lab10.Domain2.Interfaces;
using Lab10.Infrastructure2.Context; 
using Lab10.Infrastructure2.Repositories;
using Lab10.Infrastructure2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore; 
using MediatR;

namespace Lab10.Infrastructure2.Configuration;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Db context
        services.AddDbContext<Lab10Context>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        // Configuración de Validación de Token JWT
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetValue<string>("Secret");

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                    ValidAudience = jwtSettings.GetValue<string>("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });

        // Registro de los servicios del módulo de autenticación
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();
        
        // inyeccion de depdenencias
        services.AddScoped<ICourseRepository, CourseRepository>();
        
        // REGISTRO DEL SERVICIO PARA HANGFIRE (Agregado aquí)
        services.AddTransient<NotificationService>();
        
        //Registro obligatorio del contrato (Interfaz) y su implementación real
        services.AddScoped<IBackgroundJobService, BackgroundJobService>();
        
        // Configuración de MediatR para mapear automáticamente todas tus clases en Application
        // MediatR viajará a Application, buscará IAuthService y registrará TODOS tus UseCases automáticamente
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(IAuthService).Assembly));
        
        // Ya no necesitas registrar tus casos de uso uno por uno (como LoginUseCase) 
        // porque MediatR se encargará de resolver los Handlers de forma automática.
        
        
        
        return services;
    }
}