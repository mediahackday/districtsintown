using System;
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
                    Score = Math.Round(filteredByZipCode.Average(s => s.Score), 2),
                    Latitude = Math.Round(filteredByZipCode.Average(l => l.Latitude), 6),
                    Longitude = Math.Round(filteredByZipCode.Average(l => l.Longitude), 6),
                    ZipCode = code
                };
            }
        }
    }
}