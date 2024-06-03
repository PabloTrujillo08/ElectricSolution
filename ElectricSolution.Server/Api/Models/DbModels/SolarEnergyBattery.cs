using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class SolarEnergyBattery: BaseEntity
    {
        [ForeignKey("SolarEnergy")]
        public int SolarEnergyId { get; set; }
        [JsonIgnore]
        public SolarEnergy? SolarEnergy { get; set; }

        [ForeignKey("SolarBattery")]
        public int SolarBatteryId { get; set; }
        [JsonIgnore]
        public SolarBattery? SolarBattery { get; set; }
    }
}
