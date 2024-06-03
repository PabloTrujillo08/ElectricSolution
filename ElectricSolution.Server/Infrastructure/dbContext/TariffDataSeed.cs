using ElectricSolution.Server.Api.Models.DbModels;
using ElectricSolution.Server.Infrastructure.dbContext;
using System;

namespace ElectricSolution.Server.Infrastructure.dbContext
{
    public static class TariffDataSeed
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ElectricDbContext>();


            if (!context.TariffHours.Any())
            {
                context.TariffHours.AddRange(
                    new TariffHours { Name = "Tarifa Valle" },
                    new TariffHours { Name = "Tarifa Pico" },
                    new TariffHours { Name = "Tarifa Llano" },
                    new TariffHours { Name = "Tarifa Plana" }
                );

                context.SaveChanges();
            }

            var tariffHoursValleId = context.TariffHours.First(th => th.Name == "Tarifa Valle").Id;
            var tariffHoursPicoId = context.TariffHours.First(th => th.Name == "Tarifa Pico").Id;
            var tariffHoursLlanoId = context.TariffHours.First(th => th.Name == "Tarifa Llano").Id;
            var tariffHoursPlanaId = context.TariffHours.First(th => th.Name == "Tarifa Plana").Id;

            // Verifica si ya existen tarifas en la base de datos para evitar duplicados
            if (!context.Tariffs.Any())
            {
                Console.WriteLine("Inicializando datos de tarifas...");
                var tariffs = new List<Tariff>
                {
                    new Tariff
                    {
                        Name = "Tarifa Fija",
                        Description = "Precio fijo las 24 horas del día",
                        Rates = new List<TariffRate>
                        {
                            new TariffRate
                            {
                                CostPerKWh = 0.15M,
                                StartTime = new TimeSpan(0, 0, 0), // Medianoche
                                EndTime = new TimeSpan(23, 59, 59), // Fin del día
                                Description = "Todo el día",
                                TariffHoursId= tariffHoursPlanaId

                                // Esta es Tarifa Plana 
                            }
                        }
                    },
                    new Tariff
                    {
                        Name = "Tarifa por Discriminación Horaria",
                        Description = "Precios variables según el horario",
                        Rates = new List<TariffRate>
                        {
                            new TariffRate
                            {
                                CostPerKWh = 0.20M,
                                StartTime = new TimeSpan(10, 0, 0), // 10:00 AM
                                EndTime = new TimeSpan(14, 0, 0), // 2:00 PM
                                Description = "Horas Pico",
                                TariffHoursId= tariffHoursPicoId




                                // esta Es Tarifa Pico
                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.20M,
                                StartTime = new TimeSpan(18, 0, 0), // 18:00 AM
                                EndTime = new TimeSpan(22, 0, 0), // 22:00 PM
                                Description = "Horas Pico",
                                TariffHoursId= tariffHoursPicoId

                                // esta Es Tarifa Pico
                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.15M,
                                StartTime = new TimeSpan(8, 0, 0), // 8:00 AM
                                EndTime = new TimeSpan(10, 0, 0), // 10:00 AM
                                Description = "Horas Llano",
                                TariffHoursId= tariffHoursLlanoId

                                // Esta es Tarifa Llano

                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.15M,
                                StartTime = new TimeSpan(14, 0, 0), // 14:00 PM
                                EndTime = new TimeSpan(18, 0, 0), // 18:00 PM
                                Description = "Horas Llano",
                                TariffHoursId= tariffHoursLlanoId


                                // Esta es Tarifa Llano

                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.15M,
                                StartTime = new TimeSpan(22, 0, 0), // 6:00 PM
                                EndTime = new TimeSpan(0, 0, 0), // 10:00 PM
                                Description = "Horas Llano",
                                TariffHoursId= tariffHoursLlanoId

                                // Esta es Tarifa Llano

                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.10M,
                                StartTime = new TimeSpan(0, 0, 0), // Medianoche
                                EndTime = new TimeSpan(8, 0, 0), // 6:00 AM
                                Description = "Horas Valle",
                                TariffHoursId= tariffHoursValleId

                                // Esta es Tarifa Valle
                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.10M,
                                StartTime = new TimeSpan(8, 0, 0), // Medianoche
                                EndTime = new TimeSpan(10, 0, 0), // 6:00 AM
                                Description = "Horas Valle",
                                TariffHoursId= tariffHoursValleId

                                // Esta es Tarifa Valle
                            }
                        }
                    },
                    // PVPC - Mercado Regulado (sin TariffRate específicos ya que los precios son dinámicos)
                    new Tariff
                    {
                        Name = "PVPC - Mercado Regulado",
                        Description = "Tarifa dinámica del mercado regulado, los precios cambian cada hora.",
                        Rates = new List<TariffRate>
                        {
                            new TariffRate
                            {
                                CostPerKWh = 0.20M,
                                StartTime = new TimeSpan(10, 0, 0), // 10:00 AM
                                EndTime = new TimeSpan(14, 0, 0), // 2:00 PM
                                Description = "Horas Pico",
                                TariffHoursId= tariffHoursPicoId



                                // esta Es Tarifa Pico
                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.20M,
                                StartTime = new TimeSpan(18, 0, 0), // 18:00 AM
                                EndTime = new TimeSpan(22, 0, 0), // 22:00 PM
                                Description = "Horas Pico",
                                TariffHoursId= tariffHoursPicoId

                                // esta Es Tarifa Pico
                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.15M,
                                StartTime = new TimeSpan(8, 0, 0), // 8:00 AM
                                EndTime = new TimeSpan(10, 0, 0), // 10:00 AM
                                Description = "Horas Llano",
                                TariffHoursId= tariffHoursLlanoId

                                // Esta es Tarifa Llano

                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.15M,
                                StartTime = new TimeSpan(14, 0, 0), // 14:00 PM
                                EndTime = new TimeSpan(18, 0, 0), // 18:00 PM
                                Description = "Horas Llano",
                                TariffHoursId= tariffHoursLlanoId

                                // Esta es Tarifa Llano

                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.15M,
                                StartTime = new TimeSpan(22, 0, 0), // 6:00 PM
                                EndTime = new TimeSpan(0, 0, 0), // 10:00 PM
                                Description = "Horas Llano",
                                TariffHoursId= tariffHoursLlanoId

                                // Esta es Tarifa Llano

                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.10M,
                                StartTime = new TimeSpan(0, 0, 0), // Medianoche
                                EndTime = new TimeSpan(8, 0, 0), // 6:00 AM
                                Description = "Horas Valle",
                                TariffHoursId= tariffHoursValleId

                                // Esta es Tarifa Valle
                            },
                            new TariffRate
                            {
                                CostPerKWh = 0.10M,
                                StartTime = new TimeSpan(8, 0, 0), // Medianoche
                                EndTime = new TimeSpan(10, 0, 0), // 6:00 AM
                                Description = "Horas Valle",
                                TariffHoursId= tariffHoursValleId

                                // Esta es Tarifa Valle
                            }
                        }
                    }
                };

                context.Tariffs.AddRange(tariffs);
                await context.SaveChangesAsync();
            }
        }
    }
}
