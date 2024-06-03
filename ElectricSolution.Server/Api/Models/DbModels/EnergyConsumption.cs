using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class EnergyConsumption: BaseEntity
    {
       
        public DateTime Date { get; set; }
        public decimal ConsumptionKWh { get; set; }

        // Navigation property
        public int InstallationId { get; set; }
        [JsonIgnore]
        public virtual Installation? Installation { get; set; }
    }
}
