using ElectricSolution.Server.Api.Models.Enums;

namespace ElectricSolution.Server.Api.Models.DTO.ContractRequestDTO
{
    public class SolarBatteryDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public AvailablePower Capacity { get; set; }
    }
}
