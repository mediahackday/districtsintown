using System.Collections.Generic;
using DistrictsInTown.Api.Models;

namespace DistrictsInTown.Api.Repositories
{
    public class LocationRepository
    {
        public IEnumerable<Location> Get(string keyword)
        {
            return new List<Location>
            {
                // Haxelthon @ Markgrafenstraße 14, 10969 Berlin
                new Location {Latitude = 52.50524, Longitude = 13.3949, Score = 123},
                // Smarthouse @ Erich-Weinert-Straße 145, 10409 Berlin
                new Location {Latitude = 52.5439305, Longitude = 13.4401417, Score = 123}
            };
        }
    }
}