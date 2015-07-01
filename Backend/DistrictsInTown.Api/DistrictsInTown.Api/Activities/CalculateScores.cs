using System.Collections.Generic;
using System.Linq;
using DistrictsInTown.Api.Models;

namespace DistrictsInTown.Api.Activities
{
    public class CalculateScores
    {
        public IEnumerable<Place> For(IEnumerable<Place> places)
        {
            var placesList = places.ToList();
            var zipCodes = placesList.Select(z => z.ZipCode).Distinct();

            foreach (var zipCode in zipCodes)
            {
                var code = zipCode;
                var filteredByZipCode = placesList.Where(z => z.ZipCode == code).ToList();

                yield return new Place
                {
                    Score = filteredByZipCode.Average(s => s.Score),
                    Latitude = filteredByZipCode.Average(l => l.Latitude),
                    Longitude = filteredByZipCode.Average(l => l.Longitude),
                    ZipCode = code
                };
            }
        }
    }
}