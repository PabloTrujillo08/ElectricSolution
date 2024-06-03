using AutoMapper;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Api.Models.DTO;
using ElectricSolution.Server.Infrastructure.dbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using System.Net.Mail;

namespace ElectricSolution.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientEditController : ControllerBase
    {
        private readonly UserManager<Client> userManager;
        private readonly IMapper mapper;
        private readonly ElectricDbContext _context;

        public ClientEditController(UserManager<Client> userManager, IMapper mapper, ElectricDbContext context)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            _context = context;
        }

        [HttpGet("GetClientData/{userId}")]
        public async Task<IActionResult> GetClientData(string userId)
        {
            var user = await userManager.Users
            .Include(u => u.Addresses) // Incluye las direcciones asociadas al usuario
            .ThenInclude(a => a.Province)
            .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound("Usuario no encontrado.");


            var mainAddresses = user.Addresses
                    .Where(a => a.IsMainAddress)
                    .Select(a => new
                    {
                        a.Street,
                        a.DoorNumber,
                        a.City,
                        a.ZipCode,
                        ProvinceId = a.ProvinceId,
                        
                    })
                    .ToList();

            var clientData = new
            {
                user.Name,
                user.LastName,
                user.UserName,
                user.Email,
                user.PhoneNumber,
                Addresses = mainAddresses,
            };
            //var pause = 0;
            return Ok(clientData);
        }


        [HttpPut("UpdateClientData/{userId}")]
        public async Task<IActionResult> UpdateClientData(string userId, [FromBody] AddressUpdateRequestDTO model)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("Usuario no encontrado.");

           
            user.Email = model.Email;
            user.Name = model.Name;
            user.UserName = model.UserName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            var mainAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.ClientId == userId && a.IsMainAddress);

            // Si se encontró una dirección principal, actualízala con los datos del DTO
            if (mainAddress != null)
            {
                var addressDto = model.Addresses?.FirstOrDefault(a => a.IsMainAddress);
                if (addressDto != null)
                {
                    mainAddress.Street = addressDto.Street;
                    mainAddress.DoorNumber = addressDto.DoorNumber;
                    mainAddress.City = addressDto.City;
                    mainAddress.ZipCode = addressDto.ZipCode;
                    mainAddress.ProvinceId = addressDto.ProvinceId;
                }
            }

            await _context.SaveChangesAsync();

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new { Success = false, Errors = result.Errors.Select(e => e.Description) });

            return Ok(new { Success = true, Message = "Datos del usuario y su dirección principal actualizados correctamente." });
        }


    }
}
