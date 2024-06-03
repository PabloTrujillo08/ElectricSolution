using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ElectricSolution.Server.Api.Models.Enums;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class Installation: BaseEntity
    {
        public bool OwnMeter { get; set; }
        public string? Cup { get; set; }
        public AvailablePower PowerContracted { get; set; }

        // Navigation properties
        [ForeignKey("Contract")]
        public int ContractId { get; set; }
        [JsonIgnore]
        public virtual Contract? Contract { get; set; }
        
        public virtual ICollection<EnergyConsumption> EnergyConsumptions { get; set; }
        public virtual ICollection<InstallationSolarEnergy> InstallationSolarEnergies { get; set; }
    }
}
