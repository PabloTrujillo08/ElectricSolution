using ElectricSolution.Server.Api.Interfaces;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Api.Models.Services;
using ElectricSolution.Server.Infrastructure.dbContext;
using Microsoft.AspNetCore.Identity;
using ElectricSolution.Server.Api.Models.DTO;
using Microsoft.EntityFrameworkCore;
using ElectricSolution.Server.Api.Models.Enums;
using System.Text;

namespace ElectricSolution.Server.Api.Services
{
    public class ContractService : IContractService
    {
        private readonly ElectricDbContext _context;
        private readonly UserManager<Client> _userManager;
        private readonly INotificationService _notificationService;
        private readonly Random random = new Random();

        public ContractService(ElectricDbContext context, UserManager<Client> userManager, INotificationService notificationService)
        {
            _context = context;
            _userManager = userManager;
            _notificationService = notificationService;
        }

        public async Task<ServiceResponse<Contract>> CreateContractAsync(ClientRegisterContractDTO model)
        {

            var response = new ServiceResponse<Contract>();
            // return response;
            // Encuentra el cliente por ID
            Client? client = await _userManager.FindByIdAsync(model.ClientId!);
            if (client == null)
            {
                response.Success = false;
                response.Message = "Cliente no encontrado.";
                return response;
            }

            // Preparar la entidad Address (solo consideraremos la primera dirección como ejemplo)
            var addressDto = model.Addresses?.FirstOrDefault();
            var address = new Address(client, addressDto!);
            
            

            // Preparar las entidades relacionadas
            var installations = model.Installations!.Select(i => new Installation
            {
                OwnMeter = i.OwnMeter,
                Cup = GenerateRandomCUP(),
                PowerContracted = i.PowerContracted
            }).ToList();

            var solarEnergies = model.SolarEnergies!.Select(se => new SolarEnergy
            {
                Description = se.Description,
                Capacity = se.Capacity,
                Injection = se.Injection,
                Battery = se.Battery
            }).ToList();

            var solarBatteries = model.SolarBattery!.Select(sb => new SolarBattery
            {
                Description = sb.Description,
                Capacity = sb.Capacity
            }).ToList();

            // Preparar y guardar la entidad Contract
            var contract = new Contract
            {

                ClientId = client.Id,
                ContractNumber = GenerateContractNumber(),
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                BankAccount = model.BankAccount,
                PowerContracted = model.PowerContracted,
                FiscalAddress = model.FiscalAddress,
                TariffId = model.TariffId,
                Address = address,
                Installation = installations.FirstOrDefault(),
                
            };
            // Guardar SolarEnergies y SolarBatteries
            foreach (var solarEnergy in solarEnergies)
                await _context.SolarEnergies.AddAsync(solarEnergy);
           
            foreach (var solarBattery in solarBatteries)
                await _context.SolarBatteries.AddAsync(solarBattery);
            
            await _context.SaveChangesAsync();

            // Asociar SolarEnergies con SolarBatteries
            foreach (var solarEnergy in solarEnergies)
            {
                foreach (var solarBattery in solarBatteries)
                {
                    var solarEnergyBattery = new SolarEnergyBattery
                    {
                        SolarEnergyId = solarEnergy.Id,
                        SolarBatteryId = solarBattery.Id
                    };
                    _context.SolarEnergyBatteries.Add(solarEnergyBattery);
                }
            }
           


            try
            {
                await _context.Contracts.AddAsync(contract);
                await _context.SaveChangesAsync();

                // Asociar las instalaciones con el contrato
                var savedInstallation = contract.Installation;

                if (savedInstallation != null)
                {
                    foreach (var solarEnergy in solarEnergies)
                    {
                        var installationSolarEnergy = new InstallationSolarEnergy
                        {
                            InstallationId = savedInstallation.Id,
                            SolarEnergyId = solarEnergy.Id
                        };

                        // Guardar la nueva entidad InstallationSolarEnergy
                        _context.InstallationSolarEnergy.Add(installationSolarEnergy);
                    }

                    await _context.SaveChangesAsync();
                }


                // Asociar la dirección con el contrato
                address.ContractId = contract.Id;
                _context.Addresses.Update(address);
                await _context.SaveChangesAsync();

                response.Data = contract;
                response.Success = true;
                response.Message = "Contrato creado con éxito.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error al crear el contrato: {ex.Message}";
            }


            if (response.Success)
            {
                await _notificationService.CreateNotificationAsync(
                    "Has registrado un nuevo contrato!",
                    NotificationReason.NewContract,
                    contract.Id,
                    client.Id);
            }


            return response;

        }




        // Genera un CUP aleatorio siguiendo un patrón específico
        private string GenerateRandomCUP(string prefix = "ES", int length = 20)
        {
            StringBuilder cupBuilder = new StringBuilder(prefix);

            for (int i = prefix.Length; i < length; i++)
            {
                if (i % 5 == 0)
                    cupBuilder.Append((char)('A' + random.Next(0, 26))); 
                else
                    cupBuilder.Append(random.Next(0, 10).ToString()); 
            }

            return cupBuilder.ToString();
        }


        private string GenerateContractNumber()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var randomNumberString = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return "ES" + randomNumberString;
        }
    }
}
