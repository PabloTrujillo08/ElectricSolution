using ElectricSolution.Server.Api.Models.DbModels;

namespace ElectricSolution.Server.Infrastructure.dbContext
{
    public class ProvincesDataSeed
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<ElectricDbContext>())
            {
                // Inicializar Provinces, asegurándose de no duplicar la semilla
                if (!context.Provinces.Any())
                {
                    var Provinces = new List<Provinces>
                    {
                    new Provinces { Name = "A Coruña" },
                    new Provinces { Name = "Álava" },
                    new Provinces { Name = "Albacete" },
                    new Provinces { Name = "Alicante" },
                    new Provinces { Name = "Almería" },
                    new Provinces { Name = "Asturias" },
                    new Provinces { Name = "Ávila" },
                    new Provinces { Name = "Badajoz" },
                    new Provinces { Name = "Barcelona" },
                    new Provinces { Name = "Burgos" },
                    new Provinces { Name = "Cáceres" },
                    new Provinces { Name = "Cádiz" },
                    new Provinces { Name = "Cantabria" },
                    new Provinces { Name = "Castellón" },
                    new Provinces { Name = "Ciudad Real" },
                    new Provinces { Name = "Córdoba" },
                    new Provinces { Name = "Cuenca" },
                    new Provinces { Name = "Gerona" },
                    new Provinces { Name = "Granada" },
                    new Provinces { Name = "Guadalajara" },
                    new Provinces { Name = "Guipúzcoa" },
                    new Provinces { Name = "Huelva" },
                    new Provinces { Name = "Huesca" },
                    new Provinces { Name = "Islas Baleares" },
                    new Provinces { Name = "Jaén" },
                    new Provinces { Name = "La Rioja" },
                    new Provinces { Name = "Las Palmas" },
                    new Provinces { Name = "León" },
                    new Provinces { Name = "Lérida" },
                    new Provinces { Name = "Lugo" },
                    new Provinces { Name = "Madrid" },
                    new Provinces { Name = "Málaga" },
                    new Provinces { Name = "Murcia" },
                    new Provinces { Name = "Navarra" },
                    new Provinces { Name = "Orense" },
                    new Provinces { Name = "Palencia" },
                    new Provinces { Name = "Pontevedra" },
                    new Provinces { Name = "Salamanca" },
                    new Provinces { Name = "Segovia" },
                    new Provinces { Name = "Sevilla" },
                    new Provinces { Name = "Soria" },
                    new Provinces { Name = "Tarragona" },
                    new Provinces { Name = "Santa Cruz de Tenerife" },
                    new Provinces { Name = "Teruel" },
                    new Provinces { Name = "Toledo" },
                    new Provinces { Name = "Valencia" },
                    new Provinces { Name = "Valladolid" },
                    new Provinces { Name = "Vizcaya" },
                    new Provinces { Name = "Zamora" },
                    new Provinces { Name = "Zaragoza" }
                    };

                    context.Provinces.AddRange(Provinces);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
