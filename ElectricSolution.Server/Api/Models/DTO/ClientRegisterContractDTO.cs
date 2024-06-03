using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Api.Models.Enums;

namespace ElectricSolution.Server.Api.Models.DTO
{
    public class ClientRegisterContractDTO
    {
        public string? ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? BankAccount { get; set; }
        public string? ContractNumber { get; set; }
        public bool FiscalAddress { get; set; }
        public int TariffId { get; set; }
        public AvailablePower PowerContracted { get; set; }

        public List<AddressCreateRequestDTO>? Addresses { get; set; }
        public List<Installation>? Installations { get; set; }
        public List<SolarEnergy>? SolarEnergies { get; set; }
        public List<SolarBattery>? SolarBattery { get; set; }

     
    }
}
