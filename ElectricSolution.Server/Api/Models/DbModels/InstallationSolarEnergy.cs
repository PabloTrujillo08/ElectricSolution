using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class InstallationSolarEnergy: BaseEntity
    {
        public int InstallationId { get; set; } // Clave foránea a Installation
        [JsonIgnore]
        public virtual Installation? Installation { get; set; } // Propiedad de navegación a Installation

        public int SolarEnergyId { get; set; } // Clave foránea a SolarEnergy
        public virtual SolarEnergy? SolarEnergy { get; set; } // Propiedad de navegación a SolarEnergy
    }
}
