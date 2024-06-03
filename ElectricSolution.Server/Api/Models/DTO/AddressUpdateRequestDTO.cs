namespace ElectricSolution.Server.Api.Models.DTO
{
    public class AddressUpdateRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }


        public List<AddressCreateRequestDTO>? Addresses { get; set; }

        public string? ClientId { get; set; }
        public string? ContractId { get; set; }
    }
}
