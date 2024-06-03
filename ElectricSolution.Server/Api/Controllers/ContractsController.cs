using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Infrastructure.dbContext;

using ElectricSolution.Server.Api.Models.DTO.ContractRequestDTO;

namespace ElectricSolution.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly ElectricDbContext _context;

        public ContractsController(ElectricDbContext context)
        {
            _context = context;
        }

        // GET: api/Contracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractDTO>>> GetContracts()
        {
            var contracts = await _context.Contracts
                .Include(c => c.Address) // Incluir la dirección
                .Include(c => c.Tariff) // Incluir la tarifa
                .Include(c => c.Notifications) // Incluir las notificaciones
                .Include(c => c.Invoices) // Incluir las facturas
                .Include(c => c.Installation)// Incluir las instalaciones
                    .ThenInclude(inst => inst!.EnergyConsumptions)
                .Include(c => c.Installation) // Incluir las instalaciones
                    .ThenInclude(inst => inst!.InstallationSolarEnergies!)
                    .ThenInclude(solarEnergy => solarEnergy.SolarEnergy)
                     .ThenInclude(solarEnergy => solarEnergy!.SolarEnergyBatteries)
                     .ThenInclude(solarEnergyBattery => solarEnergyBattery.SolarBattery)
                .ToListAsync();


            // Mapear los contratos a ContractDto
            var contractsDto = contracts.Select(c => new ContractDTO
            {
                Id = c.Id,
                PowerContracted = c.PowerContracted,
                ContractNumber = c.ContractNumber,
                BankAccount = c.BankAccount,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                FiscalAddress = c.FiscalAddress,
                // Mapear la dirección como un objeto AddressDto excluyendo la propiedad Contract
                Address = new AddressDTO
                {
                    Street = c.Address!.Street,
                    DoorNumber = c.Address.DoorNumber,
                    City = c.Address.City,
                    ZipCode = c.Address.ZipCode,
                    ProvinceId = c.Address.ProvinceId,
                    Province = c.Address != null ? _context.Provinces.FirstOrDefault(p => p.Id == c.Address.ProvinceId)?.Name : null,
                    isMainAddress = c.Address!.IsMainAddress,
                },
                ClientId = c.ClientId,
                TariffId = c.TariffId,
                // Puedes mapear las propiedades necesarias de la tarifa
                TariffName = c.Tariff != null ? c.Tariff.Name : null,
                TariffDescription = c.Tariff != null ? c.Tariff.Description : null,
                // Mapear las notificaciones si es necesario
                Notifications = c.Notifications,
                Invoices = c.Invoices,
                Installation = c.Installation != null ? new List<Installation> { c.Installation } : null,
                SolarBattery = c.Installation?.InstallationSolarEnergies?.FirstOrDefault()?.SolarEnergy?.SolarEnergyBatteries?.FirstOrDefault()?.SolarBattery != null ? new SolarBatteryDTO
                {
                    Id = c.Installation.InstallationSolarEnergies.First().SolarEnergy!.SolarEnergyBatteries.First().SolarBattery!.Id,
                    Description = c.Installation.InstallationSolarEnergies.First().SolarEnergy!.SolarEnergyBatteries.First().SolarBattery!.Description,
                    Capacity = c.Installation.InstallationSolarEnergies.First().SolarEnergy!.SolarEnergyBatteries.First().SolarBattery!.Capacity
                } : null
               
            });

            return Ok(contractsDto);
        }


        // GET: api/Contracts/5
        [HttpGet("GetMyContract/{id}")]
        public async Task<ActionResult<IEnumerable<ContractDTO>>> GetMyContract(string id)
        {
            var contracts = await _context.Contracts
                .Where(c => c.ClientId == id)
                .Include(c => c.Address)
                .Include(c => c.Tariff)
                .Include(c => c.Notifications)
                .Include(c => c.Invoices)
                .Include(c => c.Installation)
                    .ThenInclude(inst => inst!.EnergyConsumptions)
                .Include(c => c.Installation)
                    .ThenInclude(inst => inst!.InstallationSolarEnergies!)
                        .ThenInclude(solarEnergy => solarEnergy.SolarEnergy)
                            .ThenInclude(solarEnergy => solarEnergy!.SolarEnergyBatteries)
                                .ThenInclude(solarEnergyBattery => solarEnergyBattery.SolarBattery)
                .ToListAsync();

            if (!contracts.Any())
            {
                return NotFound();
            }

            var contractDtos = contracts.Select(contract => new ContractDTO
            {
                // Mapeo de propiedades del contrato a ContractDTO
                Id = contract.Id,
                PowerContracted = contract.PowerContracted,
                ContractNumber = contract.ContractNumber,
                BankAccount = contract.BankAccount,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                FiscalAddress = contract.FiscalAddress,
                // Asegúrate de ajustar la creación de AddressDTO y otros DTOs según tu estructura
                Address = new AddressDTO
                {
                    Street = contract.Address!.Street,
                    DoorNumber = contract.Address!.DoorNumber,
                    City = contract.Address!.City,
                    ZipCode = contract.Address!.ZipCode,
                    ProvinceId = contract.Address!.ProvinceId,
                    Province = contract.Address != null ? _context.Provinces.FirstOrDefault(p => p.Id == contract.Address!.ProvinceId)?.Name : null,
                    isMainAddress = contract.Address!.IsMainAddress,
                },
                ClientId = contract.ClientId,
                TariffId = contract.TariffId,
                TariffName = contract.Tariff != null ? contract.Tariff!.Name : null,
                TariffDescription = contract.Tariff != null ? contract.Tariff!.Description : null,
                Notifications = contract.Notifications,
                Invoices = contract.Invoices, 
                Installation = contract.Installation != null ? new List<Installation> { contract.Installation } : new List<Installation>(),
                SolarBattery = contract.Installation?.InstallationSolarEnergies?.FirstOrDefault()?.SolarEnergy?.SolarEnergyBatteries?.FirstOrDefault()?.SolarBattery != null ? new SolarBatteryDTO
                {
                    Id = contract.Installation!.InstallationSolarEnergies.First().SolarEnergy!.SolarEnergyBatteries.First().SolarBattery!.Id,
                    Description = contract.Installation!.InstallationSolarEnergies.First().SolarEnergy!.SolarEnergyBatteries.First().SolarBattery!.Description,
                    Capacity = contract.Installation!.InstallationSolarEnergies.First().SolarEnergy!.SolarEnergyBatteries.First().SolarBattery!.Capacity
                } : null
            }).ToList();

            return Ok(contractDtos);
        }
    }
}
