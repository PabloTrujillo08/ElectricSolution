using ElectricSolution.Server.Api.Models.Enums;

namespace ElectricSolution.Server.Api.Models.DTO
{
    public class ContractCreationModel
    {
        public string? ClientId { get; set; }

        // Datos de la dirección
        public string? Street { get; set; }
        public string? DoorNumber { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public int ProvinceId { get; set; }

        // Datos del contrato
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? BankAccount { get; set; }

        public bool HasSolarPanels { get; set; }

        public bool Injection { get; set; } 

        public bool OwnMeter { get; set; }
        public AvailablePower Capacity { get; set; }
        public AvailablePower PowerContracted { get; set; }
        public bool FiscalAddress { get; set; }
        public int TariffId { get; set; }
    }
}
