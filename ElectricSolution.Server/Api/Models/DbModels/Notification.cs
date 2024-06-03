using ElectricSolution.Server.Api.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class Notification: BaseEntity
    {
        
        public string? Message { get; set; }
        public DateTime SentDate { get; set; }
        public bool Read { get; set; }
        public NotificationReason Reason { get; set; }

  
        public string? ClientId { get; set; }
        public virtual Client? Client { get; set; }


       
        public int? ContractId { get; set; }

        [JsonIgnore]
        [ForeignKey("ContractId")]
        public virtual Contract? Contract { get; set; }
    }

}
