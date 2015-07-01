using System.Collections.Generic;
using System.Linq;
using DistrictsInTown.Api.Models;
using DistrictsInTown.DbModel;

namespace NominatimImporter.Activities
{
    internal class SaveChangedPlaces
    {
        /// <summary>
        ///     Saves <see cref="Place" /> items with new zip codes.
        /// </summary>
        /// <param name="places">The <see cref="Place" /> items that should be saved.</param>
        public void This(IEnumerable<Place> places)
        {
            using (var container = new DistrictsInTownModelContainer())
            {
                foreach (var place in places)
                {
                    var placeToUpdate = container.Places.SingleOrDefault(p => p.Source == place.Source);

                    if (placeToUpdate == null)
                    {
                        continue;
                    }

                    placeToUpdate.Zip = place.ZipCode;
                }

                container.SaveChanges();
            }
        }
    }
}