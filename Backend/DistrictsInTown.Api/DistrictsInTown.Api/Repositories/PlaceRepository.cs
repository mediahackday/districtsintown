using System.Collections.Generic;
using System.Linq;
using DistrictsInTown.Api.Models;
using DistrictsInTown.DbModel;

namespace DistrictsInTown.Api.Repositories
{
    public class PlaceRepository
    {
        public IEnumerable<Place> Get(IList<string> keyword)
        {
            if (!keyword.Any())
            {
                return Enumerable.Empty<Place>();
            }

            var container = new DistrictsInTownModelContainer();

            return ToPlaceList(container.Places.Where(p => keyword.Contains(p.Keyword)));
        }

        private static IEnumerable<Place> ToPlaceList(IEnumerable<Places> places)
        {
            return places.Select(place => new Place
            {
                Latitude = place.Location.Latitude ?? 0D,
                Longitude = place.Location.Longitude ?? 0D,
                Score = place.Score,
                ZipCode = place.Zip
            });
        }
    }
}