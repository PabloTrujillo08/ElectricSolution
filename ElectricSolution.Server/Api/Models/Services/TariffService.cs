using ElectricSolution.Server.Api.Interfaces;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Infrastructure.dbContext;
using Microsoft.EntityFrameworkCore;

namespace ElectricSolution.Server.Api.Models.Services
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
                                 .ToListAsync();
        }
    }
}
