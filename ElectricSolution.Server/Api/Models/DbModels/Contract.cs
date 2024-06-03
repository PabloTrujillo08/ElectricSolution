using ElectricSolution.Server.Api.Models.Enums;
using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class Contract: BaseEntity
    {
       
        
        
        public AvailablePower PowerContracted { get; set; }
        public string? ContractNumber { get; set; }
        public string? BankAccount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool FiscalAddress { get; set; }
        // Navigation properties

        public string? ClientId { get; set; }
        [JsonIgnore]
        public virtual Client? Client { get; set; }
        
        public int AddressId { get; set; }
        [JsonIgnore]
        public virtual Address? Address { get; set; }
        
        public virtual ICollection<Invoice>? Invoices { get; set; }
        
        public virtual Installation? Installation { get; set; }


        public int TariffId { get; set; } // Clave foránea
        public virtual Tariff? Tariff { get; set; }


        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }


   
}
