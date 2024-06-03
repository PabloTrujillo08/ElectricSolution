using ElectricSolution.Server.Infrastructure.dbContext;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Api.Interfaces;
using ElectricSolution.Server.Api.Models.Enums;

public class NotificationService : INotificationService
{
    private readonly ElectricDbContext _context;

    public NotificationService(ElectricDbContext context)
    {
        _context = context;
    }

    public async Task CreateNotificationAsync(string message, NotificationReason reason, int? contractId, string clientId)
    {
        var notification = new Notification
        {
            Message = message,
            SentDate = DateTime.UtcNow,
            Read = false,
            Reason = reason,
            ContractId = contractId,
            ClientId = clientId
        };

        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();
    }
}