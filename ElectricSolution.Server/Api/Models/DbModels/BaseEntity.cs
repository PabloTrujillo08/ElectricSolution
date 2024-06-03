using System.ComponentModel.DataAnnotations;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
