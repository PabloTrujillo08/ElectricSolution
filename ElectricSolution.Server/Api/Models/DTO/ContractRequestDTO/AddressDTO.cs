namespace ElectricSolution.Server.Api.Models.DTO.ContractRequestDTO
{
    public class AddressDTO
    {
        public string? Street { get; set; }
        public string? DoorNumber { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public int ProvinceId { get; set; }
        public string? Province { get; set; }

        public bool isMainAddress { get; set; }
    }
}
