using System.Linq;
using NominatimImporter.Activities;

namespace NominatimImporter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var retrievePlaces = new RetrievePlaces();
            var updatePlaces = new UpdatePlaces();
            var saveChangedPlaces = new SaveChangedPlaces();

            var places = retrievePlaces.Narrowed("ipool_").ToList();
            updatePlaces.With(places);
            saveChangedPlaces.This(places);
        }
    }
}