using ElectricSolution.Server.Api.Interfaces;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Infrastructure.dbContext;
using Microsoft.EntityFrameworkCore;

namespace ElectricSolution.Server.Api.Services
{
    public class TariffService : ITariffService
    {
        private readonly ElectricDbContext _context;

        public TariffService(ElectricDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tariff>> GetAllTariffsAsync()
        {
            return await _context.Tariffs
                                .Include(t => t.Rates)
                                .Select(t => new Tariff
                                {
                                    Id = t.Id,
                                    Name = t.Name,
                                    Description = t.Description,
                                    Rates = t.Rates.OrderBy(r => r.StartTime).ToList()
                                })
                                .OrderBy(t => t.Rates.FirstOrDefault()!.StartTime)
                                .ToListAsync();
        }
       
    }
}
