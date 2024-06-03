using System.ComponentModel.DataAnnotations;

namespace ElectricSolution.Server.Api.Models.ApiRest
{
    public class ReeEsiosIndicators
    {
        [Key]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public DateTime date_utc { get; set; }
        public DateTime tz_time { get; set; }

        public int geo_id { get; set; }
        public string? geo_name { get; set; }


    }
}

//https://api.esios.ree.es/
//bbf9648b188a47b64b972b6a8d6dd31d3bd9e84b61a42b433867e73122210813