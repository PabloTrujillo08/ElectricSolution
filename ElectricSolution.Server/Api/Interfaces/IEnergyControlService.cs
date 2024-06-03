using ElectricSolution.Server.Api.Models.ApiRest;

namespace ElectricSolution.Server.Api.Interfaces
{
    public interface IEnergyControlService
    {
        Task<IEnumerable<EnergyControl>> GetEnergyControlAsync();
    }
}
