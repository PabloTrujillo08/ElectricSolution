using ElectricSolution.Server.Api.Models.DTO;

namespace ElectricSolution.Server.Api.Interfaces
{
    public interface IUserService
    {
        Task<(bool Succeeded, string ErrorMessage, Dictionary<string, string>? Errors)> RegisterAsync(ClientRegisterRequestDTO model);
    }
}
