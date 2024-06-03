using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Cors;
using ElectricSolution.Server.Api.Models.ApiRest;
using ElectricSolution.Server.Api.Mappings;

namespace ElectricSolution.Server.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReeEsiosIndicatorsController : ControllerBase
    {
     
        private readonly MappingReeEsiosIndicators _reeEsiosIndicators;


        public ReeEsiosIndicatorsController(MappingReeEsiosIndicators reeEsiosIndicators)
        {
            _reeEsiosIndicators = reeEsiosIndicators;
        }



        // GET: api/ReeEsiosIndicators
       // [HttpGet(Name = "GetReeEsiosIndicators")]
        [HttpGet("{id}", Name = "GetReeEsiosIndicators")]
        public async Task<ActionResult<IEnumerable<ReeEsiosIndicators>>> GetReeEsiosIndicators(int id)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("x-api-key", _reeEsiosIndicators.token);
                   
                    HttpResponseMessage response = await client.GetAsync($"{_reeEsiosIndicators.url}/{id}");

                    response.EnsureSuccessStatusCode();

                    // Leer y deserializar la respuesta JSON
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return Ok(responseBody);

                }
                catch (HttpRequestException ex)
                {
                    return StatusCode(500, $"Error al realizar la solicitud: {ex.Message}");
                }
            }
        }
    }
}
