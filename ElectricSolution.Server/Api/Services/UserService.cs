using AutoMapper;
using ElectricSolution.Server.Api.Interfaces;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Api.Models.DTO;
using ElectricSolution.Server.Api.Models.Enums;
using ElectricSolution.Server.Infrastructure.dbContext;
using Microsoft.AspNetCore.Identity;



public class UserService : IUserService
{
    private readonly UserManager<Client> _userManager;
    private readonly IMapper mapper;
    private readonly INotificationService _notificationService;

    public UserService(UserManager<Client> userManager, IMapper mapper, INotificationService notificationService)
    {
        _userManager = userManager;
        this.mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<(bool Succeeded, string ErrorMessage, Dictionary<string, string>? Errors)> RegisterAsync(ClientRegisterRequestDTO model)
    {
        // Verifica si el email ya está registrado
        var existingUser = await _userManager.FindByEmailAsync(model.Email!);
        if (existingUser != null)
        {
            return (false, "Correo electrónico ya registrado", new Dictionary<string, string> { { "statusCode", "MAIL_KO" } });
        }


        var newUser = mapper.Map<Client>(model);
        if (newUser.Addresses.Any())
            newUser.Addresses.First().IsMainAddress = true;

  
        // Intenta registrar el nuevo usuario
        var result = await _userManager.CreateAsync(newUser, model.Password!);
        if (!result.Succeeded)
        {
            var errors = new Dictionary<string, string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Code, error.Description);
            }
            return (false, "Error al registrar el usuario", errors);
        }


        // Intenta asignar el rol al usuario
        var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
        if (!roleResult.Succeeded)
        {
            var roleErrors = new Dictionary<string, string>();
            foreach (var error in roleResult.Errors)
            {
                roleErrors.Add(error.Code, error.Description);
            }
            return (false, "Error al asignar el rol", roleErrors);
        }


        // Enviar notificación de registro exitoso
        await _notificationService.CreateNotificationAsync(
            "Bienvenido a Calambre Energy!",
            NotificationReason.RegistrationSuccess,
            null,
            newUser.Id);


        // Si todo es correcto, retorna un resultado exitoso
        return (true, "Usuario registrado con éxito", null);
    }
}
