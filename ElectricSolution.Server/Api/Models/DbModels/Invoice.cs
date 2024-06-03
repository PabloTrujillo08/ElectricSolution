using ElectricSolution.Server.Api.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class Invoice: BaseEntity
    {
        
        public DateTime IssuedDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        // Navigation property

        public int ContractId { get; set; }
        [JsonIgnore]
        public virtual Contract? Contract { get; set; }
    }

}
