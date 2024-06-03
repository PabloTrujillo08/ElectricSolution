using ElectricSolution.Server.Api.Models.DbModels;

namespace ElectricSolution.Server.Api.Interfaces
{
    public interface ITariffService
    {
        Task<IEnumerable<Tariff>> GetAllTariffsAsync();
    }
}
