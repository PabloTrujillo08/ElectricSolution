using System.ComponentModel.DataAnnotations;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class Provinces
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
