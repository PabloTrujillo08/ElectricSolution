using ElectricSolution.Server.Api.Models.DTO;
using ElectricSolution.Server.Api.Models.DTO.ContractRequestDTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class Address : BaseEntity
    {

        public string? Street { get; set; }
        public string? DoorNumber { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }

        //public string? Province { get; set; }


        public int ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Provinces? Province { get; set; }


        public bool IsMainAddress { get; set; } = false;

        public string ClientId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual Client? Client { get; set; }

        public int? ContractId { get; set; }
        public virtual Contract? Contract { get; set; }


        public Address()
        {
            ClientId = "0";
        }
        public Address(Client client, AddressCreateRequestDTO addressDto)
        {
            ClientId = client.Id;
            Street = addressDto?.Street;
            DoorNumber = addressDto?.DoorNumber;
            City = addressDto?.City;
            ZipCode = addressDto?.ZipCode;
            ProvinceId = addressDto?.ProvinceId ?? default;
            IsMainAddress = addressDto?.IsMainAddress ?? false;
           
        }
    }
}
