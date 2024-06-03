namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class ClientInstallation: BaseEntity
    {


        // Navigation properties
     
        public virtual Client? Client { get; set; }
        public int InstallationId { get; set; } // Foreign key to Installation table
        public virtual Installation? Installation { get; set; }
    }
}
