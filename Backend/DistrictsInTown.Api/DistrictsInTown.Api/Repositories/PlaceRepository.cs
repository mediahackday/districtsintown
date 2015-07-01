using System.Collections.Generic;
using System.Linq;
using DistrictsInTown.Api.Models;
using DistrictsInTown.DbModel;

namespace DistrictsInTown.Api.Repositories
{
    public class PlaceRepository
    {
        /// <summary>
        ///     Gets a collection with all available objects of type <see cref="Place" />.
        /// </summary>
        /// <returns>A collection of <see cref="Place" /> items.</returns>
        public IEnumerable<Place> Get()
        {
            var container = new DistrictsInTownModelContainer();

            return ToPlaceList(container.Places);
        }

        /// <summary>
        ///     Gets a collection with object of type <see cref="Place" /> filtered by keywords.
        /// </summary>
        /// <param name="keyword">A list of keywords to narrow the result.</param>
        /// <returns>A collection of <see cref="Place" /> items.</returns>
        public IEnumerable<Place> Get(IList<string> keyword)
        {
            if (!keyword.Any())
            {
                return Enumerable.Empty<Place>();
            }

            using (var container = new DistrictsInTownModelContainer())
            {
                return ToPlaceList(container.Places.Where(p => keyword.Contains(p.Keyword)));
            }
        }

        private static IEnumerable<Place> ToPlaceList(IEnumerable<Places> places)
        {
            return places.Select(place => new Place
            {
                Latitude = place.Location.Latitude ?? 0D,
                Longitude = place.Location.Longitude ?? 0D,
                Score = place.Score,
                Source = place.Source,
                ZipCode = place.Zip
            });
        }
    }
}