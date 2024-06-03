using ElectricSolution.Server.Api.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class SolarBattery
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public AvailablePower Capacity { get; set; }

        [JsonIgnore]
        public virtual ICollection<SolarEnergyBattery>? SolarEnergyBatteries { get; set; }

        public SolarBattery()
        {
            SolarEnergyBatteries = new HashSet<SolarEnergyBattery>();
        }
    }

}
