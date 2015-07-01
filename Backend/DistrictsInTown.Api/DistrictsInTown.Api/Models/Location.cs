using Newtonsoft.Json;

namespace DistrictsInTown.Api.Models
{
    public class Location
    {
        [JsonProperty("lng")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("count")]
        public int Score { get; set; }
    }
}