using ElectricSolution.Server.Api.Models.DbModels;
using Microsoft.AspNetCore.Identity;

namespace ElectricSolution.Server.Infrastructure.dbContext
{
    public static class FeaturesDataSeed
    {

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ElectricDbContext>();


            // Inicializar Features, asegurándose de no duplicar la semilla
            if (!context.Features.Any())
            {
                var features = new List<Feature>
            {
                new Feature { Name = "Mostrar más opciones", Description = "Permite mostrar más de 30 opciones en listados." },
                new Feature { Name = "Acceso Anticipado", Description = "Acceso anticipado a nuevas funcionalidades." },
                new Feature { Name = "Soporte Premium", Description = "Acceso a soporte premium 24/7." }
            };

                context.Features.AddRange(features);
                await context.SaveChangesAsync();
            }

         
        }
    }
}
