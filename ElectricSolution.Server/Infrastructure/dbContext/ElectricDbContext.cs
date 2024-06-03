using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Api.Models.ApiRest;
using ElectricSolution.Server.Api.Models.Enums;



namespace ElectricSolution.Server.Infrastructure.dbContext
{
    public class ElectricDbContext : IdentityDbContext<Client>
    {
        public ElectricDbContext(DbContextOptions<ElectricDbContext> options)
            : base(options)
        {
        }
        public DbSet<EnergyControl> EnergyControl { get; set; } = default!;
        public DbSet<ReeEsiosIndicators> ReeEsiosIndicators { get; set; } = default!;



        // DbModels
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientInstallation> ClientInstallations { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractFeature> ContractFeatures { get; set; }
        public DbSet<EnergyConsumption> EnergyConsumptions { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Installation> Installations { get; set; }
        public DbSet<InstallationSolarEnergy> InstallationSolarEnergy { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Provinces> Provinces { get; set; }
        public DbSet<SolarBattery> SolarBatteries { get; set; }
        public DbSet<SolarEnergy> SolarEnergies { get; set; }
        public DbSet<SolarEnergyBattery> SolarEnergyBatteries { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<TariffHours> TariffHours { get; set; }
        public DbSet<TariffRate> TariffRates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para Contract
            modelBuilder.Entity<Contract>()
                .Property(c => c.PowerContracted)
                .HasConversion<string>();
            // Configuración para Contract
            modelBuilder.Entity<Installation>()
                .Property(c => c.PowerContracted)
                .HasConversion<string>();

            // Configuración para SolarEnergy
            modelBuilder.Entity<SolarEnergy>()
                .Property(s => s.Capacity)

                .HasConversion<string>();
            modelBuilder.Entity<SolarBattery>()
               .Property(c => c.Capacity)
               .HasConversion<string>();

            modelBuilder.Entity<TariffRate>().
                Property(tr => tr.StartTime).
                HasColumnType("time(7)");

            modelBuilder.Entity<TariffRate>()
                .Property(tr => tr.EndTime)
                .HasColumnType("time(7)");

            modelBuilder.Entity<Notification>()
                .Property(n => n.Reason)
                .HasConversion(
                    v => v.ToString(),
                    v => (NotificationReason)Enum.Parse(typeof(NotificationReason), v)
                );
            // Configuración para convertir PaymentStatus a string y viceversa
            modelBuilder.Entity<Invoice>()
                .Property(i => i.PaymentStatus)
                .HasConversion(
                    v => v.ToString(), // De PaymentStatus a string
                    v => Enum.Parse<PaymentStatus>(v)); // De string a PaymentStatus

            modelBuilder.Entity<Contract>()
            .HasOne(c => c.Address)
            .WithOne(a => a.Contract)
            .HasForeignKey<Contract>(c => c.AddressId);

        }






    }
}
