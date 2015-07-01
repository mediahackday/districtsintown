using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace FourSquareImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportVenus();
        }

        private static void WriteVenues(IList<ForesquareVenue> venues)
        {
            foreach (var venue in venues)
                WriteVenue(venue);
        }

        private static void WriteVenue(ForesquareVenue venue)
        {
            Console.WriteLine("{0}  {1},{2} {3} {4}", venue.Name, venue.Longitude, venue.Latitude, venue.ZipCode, venue.Score);
        }

        private static void ImportVenus()
        {
            int chunkSize = 50;
            int offset = 0;

            ForesquareVenueResult result;
            do
            {
                result = ExploreVenues(offset, chunkSize, "Berlin, DE", "coffee", "Café").Result;
                offset += chunkSize;

                WriteVenues(result.Venues);

                Console.WriteLine("Total results: {0}", result.TotalResults);
            }
            while (offset < result.TotalResults);
        }

        private static string CLIENT_ID = "AZQX4TS2BF0SKW5HEFVJANL5C2AMXFBKC3JPBMDZ1NBMLNX5";
        private static string CLIENT_SECRET = "NWEUHJI1AF3KRGDXKTPZGX22AML5D25FUSL4M2PIR5HELMXU";

        class ForesquareVenue
        {
            public string Name;
            public double Longitude;
            public double Latitude;
            public double Score;
            public string Keyword;
            public string Source;
            public string ZipCode;
        }

        class ForesquareVenueResult
        {
            public long TotalResults;
            public IList<ForesquareVenue> Venues;
        }

        private static bool IsInCategory(dynamic categories, string categoryName)
        {
            foreach (dynamic category in categories)
            {
                if (category.shortName == categoryName)
                    return true;
            }

            return false;
        }

        private static async Task<ForesquareVenueResult> ExploreVenues(int offset, int limit, string near, string section, string category)
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("https://api.foursquare.com/v2/") };

            var response = await httpClient.GetStringAsync("venues/explore?client_id=" + CLIENT_ID + "&client_secret=" + CLIENT_SECRET + "&offset=" + offset + "&limit=" + limit + "&near=" + near + "&section=" + section + "&v=20140101");

            dynamic responseJson = JsonConvert.DeserializeObject(response);

            var result = new ForesquareVenueResult();
            result.TotalResults = responseJson.response.totalResults;
            result.Venues = new List<ForesquareVenue>();

            foreach (var group in responseJson.response.groups)
            {
                if (group.name != "recommended")
                    continue;

                // use everything over score 7

                foreach (var groupItem in group.items)
                {
                    var venue = groupItem.venue;

                    try
                    {

                        if (!IsInCategory(venue.categories, category))
                            continue;

                        if (venue.rating < 7.0)
                            continue;

                        var venueResult = new ForesquareVenue
                        {
                            Name = venue.name,
                            Longitude = venue.location.lng,
                            Latitude = venue.location.lat,
                            ZipCode = venue.location.postalCode,
                            Keyword = "coffee",
                            Score = venue.rating,
                            Source = "foursquare_" + venue.id
                        };

                        result.Venues.Add(venueResult);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Skipped {0}", venue.id);
                    }
                }
            }

            return result;
        }
    }
}
