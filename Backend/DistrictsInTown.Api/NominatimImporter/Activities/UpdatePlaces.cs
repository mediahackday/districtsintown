using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using DistrictsInTown.Api.Models;
using Newtonsoft.Json;

namespace NominatimImporter.Activities
{
    internal class UpdatePlaces
    {
        private const string QueryUrl = "http://nominatim.openstreetmap.org/reverse?format=json&lat={0}&lon={1}";

        public void With(IEnumerable<Place> places)
        {
            foreach (var place in places)
            {
                var zipCode = GetZipCodeByCoordinates(place.Latitude, place.Longitude);
                place.ZipCode = zipCode;
            }
        }

        private static string GetZipCodeByCoordinates(double latitude, double longitude)
        {
            var url = string.Format(CultureInfo.InvariantCulture, QueryUrl, latitude, longitude);
            var httpClient = new HttpClient();

            var response = httpClient.GetStringAsync(url).Result;

            dynamic responseJson = JsonConvert.DeserializeObject(response);

            return responseJson.address.postcode.Value;
        }
    }
}