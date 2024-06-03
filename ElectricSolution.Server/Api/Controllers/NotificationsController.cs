using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Infrastructure.dbContext;

namespace ElectricSolution.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ElectricDbContext _context;

        public NotificationsController(ElectricDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetUnreadNotification/{userId}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetUnreadNotification(string userId)
        {
            var unreadNotifications = await _context.Notifications
        .Where(notification => notification.ClientId == userId && !notification.Read)
        .ToListAsync();

            if (unreadNotifications == null)
            {
                return NotFound();
            }

            return unreadNotifications;
        }

        [HttpPut("MarkAsRead/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            notification.Read = true;
            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }


    }
}
