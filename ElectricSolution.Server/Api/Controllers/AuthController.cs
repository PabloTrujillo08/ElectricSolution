using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ElectricSolution.Server.Api.Models.Indentity;
using ElectricSolution.Server.Api.Models.DbModels;

using ElectricSolution.Server.Api.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using ElectricSolution.Server.Infrastructure.dbContext;
using Microsoft.EntityFrameworkCore;
using ElectricSolution.Server.Api.Models.DTO;
using InfluxDB.Client.Api.Domain;
using ElectricSolution.Server.Api.Models;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<Client> userManager;
    private readonly SignInManager<Client> _signInManager;
    private readonly IUserService _userService;
    private readonly JwtSettings jwtSettings;
    private readonly ElectricDbContext _context;


    public AuthController(UserManager<Client> _userManager, IUserService userService, SignInManager<Client> signInManager, IOptions<JwtSettings> _jwtSettings, ElectricDbContext context)
    {
        userManager = _userManager;
        _signInManager = signInManager;
        _userService = userService;
        jwtSettings = _jwtSettings.Value;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(ClientRegisterRequestDTO model)
    {
        var (Succeeded, ErrorMessage, Errors) = await _userService.RegisterAsync(model);

        if (Succeeded)
            return Ok(new { message = "Usuario registrado con éxito", statusCode = 200 });
        else
            return BadRequest(new { message = ErrorMessage, errors = Errors, statusCode = "REGISTER_KO" });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)

    {
        Client? user = null;

        if (model.Email!.Contains("@"))
            user = await userManager.FindByEmailAsync(model.Email!);
        else
            user = await userManager.FindByNameAsync(model.Email!);


        if (user == null)
            return BadRequest(new { message = "Invalid login attempt." });


        var result = await _signInManager.PasswordSignInAsync(user, model.Password!, isPersistent: false, lockoutOnFailure: false);


        if (result.Succeeded)
        {
            var token = await GenerateToken(user!);
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var authResponse = new AuthResponse(user!, jwtToken);

            return Ok(authResponse);
        }

        return BadRequest(new { message = "Invalid login attempt." });
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "@Logout Successful!!!" });
    }

    //[HttpGet("amILoggedIn")]
    //public IActionResult AmILoggedIn()
    //{
    //    // Verifica si el usuario está autenticado
    //    var isAuthenticated = User.Identity!.IsAuthenticated;

    //    // Devuelve el estado de autenticación
    //    if (isAuthenticated)
    //    {
    //        // Opcionalmente, puede devolver información adicional del usuario aquí si es necesario
    //        return Ok(new { isAuthenticated = true, userName = User.Identity.Name });
    //    }
    //    else
    //    {
    //        return Ok(new { isAuthenticated = false });
    //    }
    //}

    private static readonly Dictionary<string, PasswordChangeAttempt> _attempts = new Dictionary<string, PasswordChangeAttempt>();

    [HttpPost]
    [Route("ModifyPassword")]
    public async Task<IActionResult> ModifyPassword(ClientResetPasswordDTO model)
    {

        if (!_attempts.ContainsKey(model.Email!))
            _attempts[model.Email!] = new PasswordChangeAttempt();

        var attempt = _attempts[model.Email!];
        
        if ((DateTime.Now - attempt.LastAttemptTime).TotalMinutes < 1)
        {
            if (attempt.Attempts >= 3)
                return BadRequest(new { message = "Has excedido el máximo de intentos. Por favor, espera un minuto antes de intentarlo de nuevo." });
        }
        else
            attempt.Attempts = 0;

        attempt.Attempts++;
        attempt.LastAttemptTime = DateTime.Now;

        var user = await userManager.FindByEmailAsync(model.Email!);

        var result = await userManager.ChangePasswordAsync(user!, model.OriginalPassword!, model.ModifiedPassword!);

        if (result.Succeeded)
        {
            await _signInManager.SignOutAsync();
            attempt.Attempts = 0; 
            return Ok(new { message = "La contraseña se ha cambiado con éxito. Se Cerrará su sesión" });
        }
        else
        {
            if (attempt.Attempts >= 3)
                return BadRequest(new { message = "Ha ocurrido un error al cambiar la contraseña y has excedido el máximo de intentos. Por favor, espera un minuto antes de intentarlo de nuevo." });
            else
                return BadRequest(new { message = "Ha ocurrido un error al cambiar la contraseña, inténtalo de nuevo." });
        }

    }





    private async Task<JwtSecurityToken> GenerateToken(Client user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                //new Claim(CustomClaimTypes.Uid, user.Id),
            }.Union(userClaims).Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

        return token;

    }

}