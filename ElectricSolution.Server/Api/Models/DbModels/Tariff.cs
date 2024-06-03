using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class Tariff : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }


        // Relación uno a muchos con TariffRate
       
        public virtual ICollection<TariffRate> Rates { get; set; } = new List<TariffRate>();
    }
}