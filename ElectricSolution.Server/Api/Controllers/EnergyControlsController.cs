using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using ElectricSolution.Server.Infrastructure.dbContext;
using Microsoft.AspNetCore.Authorization;
using ElectricSolution.Server.Api.Models.ApiRest;

using ElectricSolution.Server.Api.Interfaces;


namespace ElectricSolution.Server.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnergyControlsController : ControllerBase
    {
        private readonly ElectricDbContext _context;
       

        private readonly IEnergyControlService _energyControlService;


        public EnergyControlsController(ElectricDbContext context, IEnergyControlService energyControlService)
        {
            _context = context;
           
            _energyControlService = energyControlService;
        }

        // GET: api/EnergyControls
        [HttpGet(Name = "GetEnergyControl")]
        public async Task<IActionResult> GetEnergyControl()
        {
            try
            {
                var results = await _energyControlService.GetEnergyControlAsync();
                return Ok(results);
            }
            catch (InfluxDBQueryException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                // Manejo de otros tipos de excepciones no esperadas
                return StatusCode(500, "Un error inesperado ha ocurrido: "+ex.Message);
            }
        }

        // GET: api/EnergyControls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnergyControl>> GetEnergyControl(int id)
        {
            var energyControl = await _context.EnergyControl.FindAsync(id);

            if (energyControl == null)
            {
                return NotFound();
            }

            return energyControl;
        }

    }
}
