using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using DistrictsInTown.Api.Models;
using Newtonsoft.Json;

namespace NominatimImporter.Activities
{
    internal class UpdatePlaces
    {
        private const string QueryUrl = "http://nominatim.openstreetmap.org/reverse?format=json&lat={0}&lon={1}";

        /// <summary>
        ///     Updates a list of <see cref="Place" /> items with a zip code.
        /// </summary>
        /// <param name="places">The places that should be updated.</param>
        public void With(IEnumerable<Place> places)
        {
            foreach (var place in places.GroupBy(p => new {p.Latitude, p.Longitude}))
            {
                var zipCode = GetZipCodeByCoordinates(place.Key.Latitude, place.Key.Longitude);

                if (string.IsNullOrWhiteSpace(zipCode))
                {
                    Debug.WriteLine(DateTime.Now + " ZipCode is null or empty");
                    continue;
                }

                foreach (var p in place.Where(p => p.ZipCode == "0"))
                {
                    p.ZipCode = zipCode;
                }

                Debug.WriteLine(DateTime.Now + " ZipCode updated: " + zipCode);

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }

        private static string GetZipCodeByCoordinates(double latitude, double longitude)
        {
            try
            {
                var url = string.Format(CultureInfo.InvariantCulture, QueryUrl, latitude, longitude);
                var httpClient = new HttpClient();

                var response = httpClient.GetStringAsync(url).Result;

                dynamic responseJson = JsonConvert.DeserializeObject(response);

                return responseJson.address.postcode.Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}