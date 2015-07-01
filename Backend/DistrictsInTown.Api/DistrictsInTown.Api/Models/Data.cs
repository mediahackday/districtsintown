using System.Collections.Generic;
using Newtonsoft.Json;

namespace DistrictsInTown.Api.Models
{
    public class Data
    {
        [JsonProperty("max")]
        public int Max { get; set; }

        // TODO: max 40k items
        [JsonProperty("data")]
        public IEnumerable<Place> Places { get; set; }
    }
}