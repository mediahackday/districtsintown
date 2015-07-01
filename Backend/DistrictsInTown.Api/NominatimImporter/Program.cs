using System.Globalization;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
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

            var places = retrievePlaces.Narrowed().ToList();
            updatePlaces.With(places);
            saveChangedPlaces.This(places);
        }
    }
}