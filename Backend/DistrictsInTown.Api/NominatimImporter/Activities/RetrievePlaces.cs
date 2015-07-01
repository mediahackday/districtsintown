using System;
using System.Collections.Generic;
using System.Linq;
using DistrictsInTown.Api.Models;
using DistrictsInTown.Api.Repositories;

namespace NominatimImporter.Activities
{
    internal class RetrievePlaces
    {
        /// <summary>
        ///     Gets a narrowed list of <see cref="Place" /> items that starts with the specified expression.
        /// </summary>
        /// <param name="startsWith">The search criteria.</param>
        /// <returns>A narrowed list of <see cref="Place" /> items.</returns>
        public IEnumerable<Place> Narrowed(string startsWith)
        {
            var repository = new PlaceRepository();

            return repository.Get()
                .Where(p => p.Source.StartsWith(startsWith, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}