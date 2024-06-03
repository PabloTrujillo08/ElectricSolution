namespace ElectricSolution.Server.Api.Models.DTO
{
    public class AddressCreateRequestDTO

    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? DoorNumber { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public int ProvinceId { get; set; }
        public bool IsMainAddress { get; set; } = false;

    }
}
