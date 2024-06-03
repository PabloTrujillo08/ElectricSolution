namespace ElectricSolution.Server.Api.Models.ApiRest
{
    using System;

    using InfluxDB.Client;
    using InfluxDB.Client.Core.Flux.Domain;

    public static class FluxQueryBuilder
    {
        public static string FromBucket(this string source, string bucket) => $"{source}from(bucket: \"{bucket}\")";
        public static string Range(this string source, TimeSpan range) => $"{source} |> range(start: -{range.TotalSeconds}s)";
        public static string FilterMeasurement(this string source, string measurement) => $"{source} |> filter(fn: (r) => r._measurement == \"{measurement}\")";


        public static string FilterFields(this string source, params string[] fields)
        {
            if (fields == null || fields.Length == 0)
            {
                return source;
            }

            string condition = string.Join(" or ", fields.Select(field => $"r._field == \"{field}\""));
            return $"{source} |> filter(fn: (r) => {condition})";
        }

        public static string Yield(this string source, string name) => $"{source} |> yield(name: \"{name}\")";
    }


}
