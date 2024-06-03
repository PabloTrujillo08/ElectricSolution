using ElectricSolution.Server.Api.Models.Enums;

namespace ElectricSolution.Server.Api.Interfaces
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(string message, NotificationReason reason, int? contractId, string clientId);
    }
}
