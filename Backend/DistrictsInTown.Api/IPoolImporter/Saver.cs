using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistrictsInTown.DbModel;

namespace IPoolImporter
{
    class Saver
    {
        public void Save(Dictionary<string, List<News>> news)
        {
            decimal min = news.Min(n => n.Value.Min(m => m.RawScore));
            decimal max = news.Max(n => n.Value.Max(m => m.RawScore));

            using (var dbContext = new DistrictsInTownModelContainer())
            {
                foreach (var district in news)
                    AddArticlesToDB(dbContext, district, min, max);

                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.StackTrace);
                }

                Console.WriteLine("Total results: {0}", news.Sum(n => n.Value.Count));
            }
        }

        private void AddArticlesToDB(DistrictsInTownModelContainer dbContext, KeyValuePair<string, List<News>> district, decimal min, decimal max)
        {
            dbContext.Places.RemoveRange(dbContext.Places.Where(p => p.Source.StartsWith("ipool_")));
            foreach (var news in district.Value)
            {
                
                dbContext.Places.Add(new Places
                {
                    Location = DbGeography.FromText(district.Key),
                    Keyword = "popular",
                    Score = (double) news.GetScore(min, max),
                    Source = "ipool_" + news.ID,
                    Zip = Program.PLZ.ContainsKey(district.Key) ? Program.PLZ[district.Key] : "10409"
                });
            }
        }
    }
}
