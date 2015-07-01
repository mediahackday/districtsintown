using System.Collections.Generic;
using System.Linq;
using DistrictsInTown.Api.Models;
using DistrictsInTown.DbModel;

namespace DistrictsInTown.Api.Repositories
{
    public class PlaceRepository
    {
        public IEnumerable<Place> Get(string keyword)
        {
            var container = new DistrictsInTownModelContainer();

            return string.IsNullOrWhiteSpace(keyword)
                ? ToPlaceList(container.Places.ToList())
                : ToPlaceList(container.Places.Where(p => p.Keyword == keyword)).ToList();
        }

        private static IEnumerable<Place> ToPlaceList(IEnumerable<Places> places)
        {
            return places.Select(place => new Place
            {
                Latitude = place.Location.Latitude ?? 0D,
                Longitude = place.Location.Longitude ?? 0D,
                Score = place.Score
            });
        }
    }
}