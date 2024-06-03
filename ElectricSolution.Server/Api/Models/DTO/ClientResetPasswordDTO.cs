using System.ComponentModel.DataAnnotations;

namespace ElectricSolution.Server.Api.Models.DTO
{
    public class ClientResetPasswordDTO
    {

        public string? Email { get; set; }

        public string? OriginalPassword { get; set; }

        public string? ModifiedPassword { get; set; }
        public string? ConfirmedPassword { get; set; }
    }
}
