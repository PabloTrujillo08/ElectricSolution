using ElectricSolution.Server.Api.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class SolarEnergy: BaseEntity
    {

        public string? Description { get; set; }
        public AvailablePower Capacity { get; set; }
        public bool Injection { get; set; }
        public bool Battery { get; set; }

        // Propiedad de navegación actualizada
        [JsonIgnore]
        public virtual ICollection<InstallationSolarEnergy>? InstallationSolarEnergies { get; set; }

        public virtual ICollection<SolarEnergyBattery> SolarEnergyBatteries { get; set; }

        public SolarEnergy()
        {
            SolarEnergyBatteries = new HashSet<SolarEnergyBattery>();
        }
    }
}
