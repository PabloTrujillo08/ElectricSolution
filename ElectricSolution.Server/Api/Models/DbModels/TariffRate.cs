using System;
using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class TariffRate : BaseEntity
    {
        public int TariffId { get; set; }
        [JsonIgnore]
        public virtual Tariff? Tariff { get; set; }

        public decimal CostPerKWh { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        
        public string? Description { get; set; }

       
        public int TariffHoursId { get; set; }
        // Propiedad de navegación hacia TariffHours
        public virtual TariffHours? TariffHours { get; set; }
    }
}