using ElectricSolution.Server.Infrastructure.dbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Api.Interfaces;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ElectricSolution.Server.Api.Models.Indentity;
using ElectricSolution.Server.Api.Services;
using ElectricSolution.Server.Api.Mappings;

namespace ElectricSolution.Server.Settings
{
    public static class BuilderProgram
    {
        public static WebApplication ConfigureBuilder(WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;
            IConfiguration configuration = builder.Configuration;

            AddGeneralConfigurationServices(services, configuration);

            AddInjections(services, configuration);

            return builder.Build();
        }

        private static void AddGeneralConfigurationServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); // auto mapper

            var influxDBSettings = new MappingInfluxDBSettings();
            configuration.GetSection("InfluxDB").Bind(influxDBSettings);
            services.AddSingleton(influxDBSettings);

            var reeEssiosSettings = new MappingReeEsiosIndicators();
            configuration.GetSection("Esios").Bind(reeEssiosSettings);
            services.AddSingleton(reeEssiosSettings);

            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy", builder =>
                {
                    builder.WithOrigins("*") // Especifica aquí los orígenes permitidos en producción
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }

        private static void AddInjections(IServiceCollection services, IConfiguration configuration)
        {
            AddDbInjections(services, configuration);
            AddServicesInjections(services, configuration);
            // Aquí es donde configuramos ASP.NET Core Identity
            AddIdentityInjections(services, configuration);
        }

        private static void AddDbInjections(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ElectricDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        }

        private static void AddServicesInjections(IServiceCollection services, IConfiguration configuration)
        {

            // Aquí puedes añadir tus inyecciones de servicios y repositorios
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<ITariffService, TariffService>();
            services.AddScoped<IEnergyControlService, EnergyControlService>();
            services.AddScoped<INotificationService, NotificationService>();

        }

        private static void AddIdentityInjections(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings")); // Configuración de JwtSettings en appsettings.json

            services.AddIdentity<Client, IdentityRole>(options =>
            {
                // Configuración de opciones de Identity aquí si es necesario
            })
                .AddEntityFrameworkStores<ElectricDbContext>()
                .AddDefaultTokenProviders();
            // Nota: Configura aquí cualquier otro servicio relacionado con la autenticación o autorización según sea necesario
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
                };
            });

        }
    }
}
