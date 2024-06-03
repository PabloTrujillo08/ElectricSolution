using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class TariffHours: BaseEntity
    {
        public string? Name { get; set; } // Valle, Pico, Llano, Plana
    }
}
