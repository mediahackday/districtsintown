using DistrictsInTown.DbModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
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
            ImportVenus("coffee", new[] { "Café", "Coffee Shops" }, "coffee");
            //ImportVenus("park", new[] { "Park", }, "park");
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

        private static void ImportVenus(string section, string[] categories, string keyword)
        {
            using (var dbContext = new DistrictsInTownModelContainer())
            {
                int chunkSize = 50;
                int offset = 0;

                ForesquareVenueResult result;
                do
                {
                    result = ExploreVenues(offset, chunkSize, "Berlin, DE", section, categories, keyword).Result;
                    offset += chunkSize;

                    WriteVenues(result.Venues);

                    foreach (var venue in result.Venues)
                        AddVenueToDatabase(dbContext, venue);

                    try
                    {
                        dbContext.SaveChanges();
                    }
                    catch (Exception error)
                    {

                    }

                    Console.WriteLine("Total results: {0}", result.TotalResults);
                }
                while (offset < result.TotalResults);
            }
        }

        private static void AddVenueToDatabase(DistrictsInTownModelContainer dbContext, ForesquareVenue venue)
        {
            if (String.IsNullOrEmpty(venue.ZipCode))
                return;

            dbContext.Places.Add(new Places
            {
                Location = DbGeography.FromText(String.Format("POINT ({0} {1})", venue.Longitude, venue.Latitude)),
                Keyword = venue.Keyword,
                Score = venue.Score,
                Source = venue.Source,
                Zip = venue.ZipCode
            });
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

        private static bool IsInCategory(dynamic categories, string[] categoryNames)
        {
            var names = new HashSet<string>(categoryNames);

            foreach (dynamic category in categories)
            {
                if (names.Contains((string)category.shortName))
                    return true;
            }

            Console.WriteLine("Skipped categories {0}", String.Join(" ", ((IEnumerable<dynamic>)categories).Select(n => n.shortName)));
            return false;
        }

        private static async Task<ForesquareVenueResult> ExploreVenues(int offset, int limit, string near, string section, string[] categories, string keyword)
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

                foreach (var groupItem in group.items)
                {
                    var venue = groupItem.venue;

                    try
                    {
                        if (!IsInCategory(venue.categories, categories))
                            continue;

                        if (venue.rating < 7.0)
                            continue;

                        var venueResult = new ForesquareVenue
                        {
                            Name = venue.name,
                            Longitude = venue.location.lng,
                            Latitude = venue.location.lat,
                            ZipCode = venue.location.postalCode,
                            Keyword = keyword,
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
