using ElectricSolution.Server.Api.Models.DbModels;
using InfluxDB.Client.Api.Domain;
using Newtonsoft.Json.Linq;

namespace ElectricSolution.Server.Api.Models.Indentity
{
    public class AuthResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public AuthResponse(){}

        public AuthResponse(Client user, string token)
        {
            UserId = user!.Id;
            Token = token;
            Email = user.Email!;
            Username = user.UserName!;
            Message = "@Login successful!";
        }

    }


}

