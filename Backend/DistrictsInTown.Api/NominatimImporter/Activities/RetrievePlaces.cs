using System;
using System.Collections.Generic;
using System.Linq;
using DistrictsInTown.Api.Models;
using DistrictsInTown.Api.Repositories;

namespace NominatimImporter.Activities
{
    internal class RetrievePlaces
    {
        public IEnumerable<Place> Narrowed()
        {
            var repository = new PlaceRepository();

            return repository.Get()
                .Where(p => p.Source.StartsWith("ipool_", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}