using Newtonsoft.Json;

namespace DistrictsInTown.Api.Models
{
    public class Place
    {
        [JsonProperty("lng")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("count")]
        public double Score { get; set; }

        [JsonIgnore]
        public string Source { get; set; }

        [JsonIgnore]
        public string ZipCode { get; set; }
    }
}