namespace ElectricSolution.Server.Api.Models.DTO
{
    public class ClientRegisterRequestDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Vat { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreationDate { get; set; }
        


        // DTO Data Transfer Object 
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }



        public List<AddressCreateRequestDTO>? Addresses { get; set; }


        // Propiedad para el nombre de usuario identity 
        public string? UserName
        {
            get;
            set;
        }
    }
}
