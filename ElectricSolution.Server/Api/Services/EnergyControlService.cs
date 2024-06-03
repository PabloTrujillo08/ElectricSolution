using ElectricSolution.Server.Api.Interfaces;
using Microsoft.Extensions.Options;
using ElectricSolution.Server.Api.Mappings;
using ElectricSolution.Server.Api.Models.ApiRest;
using InfluxDB.Client;

// Definición de la excepción personalizada
public class InfluxDBQueryException : Exception
{
    public InfluxDBQueryException(string message) : base(message) { }
}


namespace ElectricSolution.Server.Api.Services
{
    public class EnergyControlService: IEnergyControlService
    {
        private readonly MappingInfluxDBSettings _influxDBSettings;

        public EnergyControlService(MappingInfluxDBSettings influxDBSettings)
        {
            _influxDBSettings = influxDBSettings;
        }

        public async Task<IEnumerable<EnergyControl>> GetEnergyControlAsync()
        {
            var energyMeter = new EnergyControlData();

            var options = new InfluxDBClientOptions.Builder()
               .Url(_influxDBSettings.Url)
               .Authenticate(_influxDBSettings.Username, _influxDBSettings.Password?.ToCharArray())
               .Org(_influxDBSettings.Org)
               .Build();
            string query = string.Empty
                .FromBucket("javier")
                .Range(TimeSpan.FromSeconds(5))
                .FilterMeasurement("POTENCIAS")
                .FilterFields(energyMeter._now)
                .Yield("data");
            try
            {
                using (var client = new InfluxDBClient(options))
                {
                    var fluxClient = client.GetQueryApi();
                    var tables = await fluxClient.QueryAsync(query, _influxDBSettings.Org);
                    var tempResults = new Dictionary<DateTime, EnergyControl>();

                    foreach (var table in tables)
                    {
                        foreach (var record in table.Records)
                        {
                            var timestamp = record.GetTime().GetValueOrDefault().ToDateTimeOffset().DateTime;
                            if (!tempResults.ContainsKey(timestamp))
                            {
                                tempResults[timestamp] = new EnergyControl
                                {
                                    Date = timestamp
                                };
                            }

                            var energyControl = tempResults[timestamp];
                            var field = record.GetField();
                            // Convierte el valor al tipo correcto
                            var value = Convert.ChangeType(record.GetValue(), record.GetValue().GetType());

                            // Utiliza reflection para asignar valores de forma dinámica
                            var energyPropertyModel = typeof(EnergyControl).GetProperty(field.ToLower());

                            if (energyPropertyModel != null && value != null)
                            {
                                // Asigna el valor al campo correspondiente
                                energyPropertyModel.SetValue(energyControl, Convert.ChangeType(value, energyPropertyModel.PropertyType), null);


                                Console.WriteLine($"{record.GetTime()}: {record.GetField()}={record.GetValue()}");
                            }
                        }
                    }

                    return tempResults.Values.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar InfluxDB: {ex.Message}");
                throw new InfluxDBQueryException($"Error interno al consultar InfluxDB: {ex.Message}");
            }
            
        }
    }
}
