using ElectricSolution.Server.Api.Interfaces;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Api.Models.DTO;
using ElectricSolution.Server.Api.Models.Enums;
using ElectricSolution.Server.Api.Models.Indentity;
using ElectricSolution.Server.Api.Services;
using ElectricSolution.Server.Infrastructure.dbContext;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectricSolution.Server.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterContractController : ControllerBase
    {

        private readonly IContractService _contractService;
        private readonly ITariffService _tariffService;
       

        private readonly ElectricDbContext _context;

        public RegisterContractController(IContractService contractService, ITariffService tariffService, ElectricDbContext context)
        {

            _contractService = contractService;
            _tariffService = tariffService;
            _context = context;
        }
        [HttpPost("AddContract")]
        public async Task<IActionResult> RegisterContract([FromBody] ClientRegisterContractDTO model)
        {
            if (model == null)
            {
                return BadRequest("Modelo de contrato inválido.");
            }

            var serviceResponse = await _contractService.CreateContractAsync(model);

            if (serviceResponse.Success)
            {
                return Ok(new { serviceResponse.Success, serviceResponse.Message });
            }
            else
            {
                return BadRequest(serviceResponse.Message);
            }
        }

       

        [HttpGet("GetAllTariffs")]
        public async Task<IActionResult> GetAllTariffs()
        {
            var tariffs = await _tariffService.GetAllTariffsAsync();
            return Ok(tariffs);
        }


        [HttpGet("GetAllProvinces")]
        public async Task<ActionResult<IEnumerable<Provinces>>> GetAllProvinces()
        {
            return await _context.Provinces.ToListAsync();
        }



        [HttpGet("AvailablePowers")]
        public IActionResult GetAvailablePowers()
        {
            var powers = Enum.GetValues(typeof(AvailablePower))
                             .Cast<AvailablePower>()
                             .Select(power => new
                             {
                                 Value = (int)power,
                                 Description = power.ConvertPowerEnumToString()
                             });

            return Ok(powers);
        }

        [HttpGet("AvailableBatteryModels")]
        public IActionResult GetAvailableBatteryModels()
        {
            var models = Enum.GetValues(typeof(BatteryModels))
                             .Cast<BatteryModels>()
                             .Select(model => new
                             {
                                 Value = model.ToString(),
                                 Description = model.ConvertBatteryModelEnumToString()
                             });

            return Ok(models);
        }

        [HttpGet("AvailableInverterModels")]
        public IActionResult GetAvailableInverterModels()
        {
            var models = Enum.GetValues(typeof(InverterModels))
                             .Cast<InverterModels>()
                             .Select(model => new
                             {
                                 Value = model.ToString(),
                                 Description = model.ConvertInverterModelEnumToString()
                             });

            return Ok(models);
        }
    }
}
