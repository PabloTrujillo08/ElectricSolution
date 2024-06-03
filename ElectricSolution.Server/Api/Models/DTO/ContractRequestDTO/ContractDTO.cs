using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Api.Models.Enums;
using static ElectricSolution.Server.Api.Controllers.ContractsController;

namespace ElectricSolution.Server.Api.Models.DTO.ContractRequestDTO
{
    public class ContractDTO
    {
        public int Id { get; set; }
        public AvailablePower PowerContracted { get; set; }
        public string? ContractNumber { get; set; }
        public string? BankAccount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool FiscalAddress { get; set; }
        public AddressDTO? Address { get; set; }
        public string? ClientId { get; set; }
        public int TariffId { get; set; }
        public string? TariffName { get; set; }
        public string? TariffDescription { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
        public ICollection<Installation>? Installation { get; set; }
        public SolarBatteryDTO? SolarBattery { get; set; } = null;
    }
}
