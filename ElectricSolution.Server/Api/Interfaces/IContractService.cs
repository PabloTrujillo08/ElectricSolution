using ElectricSolution.Server.Api.Models.Services;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Api.Models.DTO;

namespace ElectricSolution.Server.Api.Interfaces
{
    public interface IContractService
    {
        Task<ServiceResponse<Contract>> CreateContractAsync(ClientRegisterContractDTO model);
    }
}
