using ElectricSolution.Server.Api.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class Client : IdentityUser
    {

        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Vat { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();


        public virtual ICollection<Contract>? Contracts { get; set; }
        public virtual ICollection<ClientInstallation>? ClientInstallations { get; set; }

        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}

