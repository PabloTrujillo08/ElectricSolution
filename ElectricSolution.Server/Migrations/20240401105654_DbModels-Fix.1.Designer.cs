﻿// <auto-generated />
using System;
using ElectricSolution.Server.Infrastructure.dbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ElectricSolution.Server.Migrations
{
    [DbContext(typeof(ElectricDbContext))]
    [Migration("20240401105654_DbModels-Fix.1")]
    partial class DbModelsFix1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DoorNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId1");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Client", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoorNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.ClientInstallation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("InstallationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId1");

                    b.HasIndex("InstallationId");

                    b.ToTable("ClientInstallations");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.ClientSolarEnergy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SolarEnergyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId1");

                    b.HasIndex("SolarEnergyId");

                    b.ToTable("ClientSolarEnergies");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("BankAccount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Certificate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FarmName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PowerContracted")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("ClientId1");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.EnergyConsumption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ConsumptionKWh")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("InstallationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstallationId");

                    b.ToTable("EnergyConsumptions");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Installation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<bool>("OwnMeter")
                        .HasColumnType("bit");

                    b.Property<int>("PowerContracted")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("Installations");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("IssuedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Read")
                        .HasColumnType("bit");

                    b.Property<int>("Reason")
                        .HasColumnType("int");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId1");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.SolarEnergy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Capacity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SolarEnergies");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.EnergyControl", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("ac_ao")
                        .HasColumnType("real");

                    b.Property<float>("ac_vo")
                        .HasColumnType("real");

                    b.Property<float>("act_pw_o")
                        .HasColumnType("real");

                    b.Property<int>("consu_pw")
                        .HasColumnType("int");

                    b.Property<float>("dc1_a")
                        .HasColumnType("real");

                    b.Property<float>("dc1_pw")
                        .HasColumnType("real");

                    b.Property<float>("dc1_v")
                        .HasColumnType("real");

                    b.Property<float>("dc2_a")
                        .HasColumnType("real");

                    b.Property<float>("dc2_pw")
                        .HasColumnType("real");

                    b.Property<float>("dc2_v")
                        .HasColumnType("real");

                    b.Property<float>("dc_pw")
                        .HasColumnType("real");

                    b.Property<float>("ef")
                        .HasColumnType("real");

                    b.Property<float>("energ_exp")
                        .HasColumnType("real");

                    b.Property<float>("energ_imp")
                        .HasColumnType("real");

                    b.Property<float>("factpw_o")
                        .HasColumnType("real");

                    b.Property<float>("freq_o")
                        .HasColumnType("real");

                    b.Property<float>("grid_a")
                        .HasColumnType("real");

                    b.Property<float>("grid_actpw")
                        .HasColumnType("real");

                    b.Property<float>("grid_exp_pw")
                        .HasColumnType("real");

                    b.Property<float>("grid_factpw")
                        .HasColumnType("real");

                    b.Property<float>("grid_freq")
                        .HasColumnType("real");

                    b.Property<float>("grid_imp_pw")
                        .HasColumnType("real");

                    b.Property<float>("grid_reapw")
                        .HasColumnType("real");

                    b.Property<float>("grid_v")
                        .HasColumnType("real");

                    b.Property<float>("pe_pw")
                        .HasColumnType("real");

                    b.Property<float>("precio_exced")
                        .HasColumnType("real");

                    b.Property<float>("precio_pvpc")
                        .HasColumnType("real");

                    b.Property<float>("rea_pw_o")
                        .HasColumnType("real");

                    b.Property<float>("state_m")
                        .HasColumnType("real");

                    b.Property<float>("temp")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.ToTable("EnergyControl");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.ReeEsiosIndicators", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_utc")
                        .HasColumnType("datetime2");

                    b.Property<int>("geo_id")
                        .HasColumnType("int");

                    b.Property<string>("geo_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("tz_time")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("ReeEsiosIndicators");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Address", b =>
                {
                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Client", "Client")
                        .WithMany("Addresses")
                        .HasForeignKey("ClientId1");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.ClientInstallation", b =>
                {
                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Client", "Client")
                        .WithMany("ClientInstallations")
                        .HasForeignKey("ClientId1");

                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Installation", "Installation")
                        .WithMany()
                        .HasForeignKey("InstallationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Installation");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.ClientSolarEnergy", b =>
                {
                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Client", "Client")
                        .WithMany("ClientSolarEnergies")
                        .HasForeignKey("ClientId1");

                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.SolarEnergy", "SolarEnergy")
                        .WithMany("ClientSolarEnergies")
                        .HasForeignKey("SolarEnergyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("SolarEnergy");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Contract", b =>
                {
                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Address", "Address")
                        .WithOne("Contract")
                        .HasForeignKey("ElectricSolution.Server.Api.Models.DbModels.Contract", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Client", "Client")
                        .WithMany("Contracts")
                        .HasForeignKey("ClientId1");

                    b.Navigation("Address");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.EnergyConsumption", b =>
                {
                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Installation", "Installation")
                        .WithMany("EnergyConsumptions")
                        .HasForeignKey("InstallationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Installation");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Installation", b =>
                {
                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Contract", "Contract")
                        .WithMany("Installations")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Invoice", b =>
                {
                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Contract", "Contract")
                        .WithMany("Invoices")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Notification", b =>
                {
                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Client", "Client")
                        .WithMany("Notifications")
                        .HasForeignKey("ClientId1");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Client", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Client", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Client", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ElectricSolution.Server.Api.Models.DbModels.Client", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Address", b =>
                {
                    b.Navigation("Contract");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Client", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("ClientInstallations");

                    b.Navigation("ClientSolarEnergies");

                    b.Navigation("Contracts");

                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Contract", b =>
                {
                    b.Navigation("Installations");

                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.Installation", b =>
                {
                    b.Navigation("EnergyConsumptions");
                });

            modelBuilder.Entity("ElectricSolution.Server.Api.Models.DbModels.SolarEnergy", b =>
                {
                    b.Navigation("ClientSolarEnergies");
                });
#pragma warning restore 612, 618
        }
    }
}