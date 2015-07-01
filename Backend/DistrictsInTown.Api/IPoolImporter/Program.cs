using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPoolImporter.Client;

namespace IPoolImporter
{
    class Program
    {
        internal const string API_KEY = "mediahackday";
        internal const string API_SECRET = "1hackpool!";
        internal const string BASEURL = "https://ipool.s.asideas.de:443/api/v3/";


        internal static readonly Dictionary<string, string> Bezirke = new Dictionary<string, string>()
        {
            { "Mitte", "POINT (52.516666666667 13.366666666667)" },
		    { "friedrichshain", "POINT (52.5 13.402222222222)" },
		    { "Friedrichshain-Kreuzberg", "POINT (52.5 13.402222222222)" },
		    { "kreuzberg", "POINT (52.5 13.402222222222)" },
		    { "Pankow", "POINT (52.568888888889 13.402222222222)" },
		    { "charlottenburg", "POINT (52.498888888889 13.285)" },
		    { "OTS_Charlottenburg-Wilmersdorf", "POINT (52.498888888889 13.285)" },
		    { "Charlottenburg-Wilmersdorf", "POINT (52.498888888889 13.285)" },
		    { "Spandau", "POINT (52.533333333333 13.166666666667)" },
		    { "spandau", "POINT (52.533333333333 13.166666666667)" },
		    { "steglitz", "POINT (52.433333333333 13.25)" },
		    { "zehlendorf", "POINT (52.433333333333 13.25)" },
		    { "Steglitz-Zehlendorf", "POINT (52.433333333333 13.25)" },
		    { "tempelhof", "POINT (52.466666666667 13.383333333333)" },
		    { "Tempelhof-SchÃ¶neberg", "POINT (52.466666666667 13.383333333333)" },
		    { "Tempelhof-Schöneberg", "POINT (52.466666666667 13.383333333333)" },
		    { "NeukÃ¶lln", "POINT (52.483333333333 13.45)" },
		    { "neukolln", "POINT (52.483333333333 13.45)" },
		    { "treptow", "POINT (52.45 13.566666666667)" },
		    { "Treptow-Köpenick", "POINT (52.45 13.566666666667)" },
		    { "OTS_Treptow-KÃ¶penick", "POINT (52.45 13.566666666667)" },
		    { "penick", "POINT (52.45 13.566666666667)" },
		    { "köpenick", "POINT (52.45 13.566666666667)" },
		    { "kopenick", "POINT (52.45 13.566666666667)" },
		    { "Treptow-KÃ¶penick", "POINT (52.45 13.566666666667)" },
		    { "marzahn", "POINT (52.539722222222 13.584166666667)" },
		    { "Marzahn-Hellersdorf", "POINT (52.539722222222 13.584166666667)" },
		    { "hellersdorf", "POINT (52.539722222222 13.584166666667)" },
		    { "Lichtenberg", "POINT (52.533333333333 13.5)" },
		    { "lichtenberg", "POINT (52.533333333333 13.5)" },
		    { "Reinickendorf", "POINT (52.566666666667 13.35)" },
 		    { "reinickendorf", "POINT (52.566666666667 13.35)" },
       }; 
        

        static void Main(string[] args)
        {
            var news = GetNewsForDistricts();
            new Saver().Save(news);
        }

        private static Dictionary<string, List<News>> GetNewsForDistricts()
        {
            var client = new ArticleClient();
            Dictionary<string, List<News>> news = new Dictionary<string, List<News>>();
            foreach (var kv in Bezirke)
            {
                if (!news.ContainsKey(kv.Value))
                {
                    Console.WriteLine(kv.Key);
                    news[kv.Value] = new List<News>();
                    var districtNews = client.GetNewsForDistrict(kv.Key);
                    news[kv.Value].AddRange(districtNews);
                }
            }
            return news;
        }

        private static void GetHistogram()
        {
            var client = new ArticleClient();
            Dictionary<string, int> hist = new Dictionary<string, int>();
            foreach (var district in Bezirke.Keys)
            {
                Console.WriteLine(district);
                client.GetHistogram(district, hist);
               
            }
            Directory.CreateDirectory("C:\\temp\\keywords\\");
            string text = String.Join("\r\n", hist.OrderBy(r => r.Value).Select(r => r.Key + ": " + r.Value).ToArray());
            File.AppendAllText("C:\\temp\\keywords\\histogram.txt", text);
            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void GetKeywordsForDistricts()
        {
            var client = new ArticleClient();
            foreach (var district in Bezirke.Keys)
            {
                Console.WriteLine(district);
                var result = client.GetKeywordsForDistrict(district);
                string text = String.Join("\r\n", result.OrderBy(r => r).ToArray());
                Directory.CreateDirectory("C:\\temp\\keywords\\");
                File.AppendAllText("C:\\temp\\keywords\\" + district + ".txt", text);
            }
            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void GetNumArticlesForDistricts()
        {
            var client = new ArticleClient();
            foreach (var district in Bezirke.Keys)
            {
                client.GetNumArticlesForDistrict(district);
            }
            Console.ReadLine();
        }
    }

    internal class News
    {
        public string ID { get; set; }
        public decimal RawScore { get; set; }

        public decimal GetScore(decimal min, decimal max)
        {
            var nulledScore = RawScore - min;
            var multiplier = 10/(max - min);
            return nulledScore*multiplier;
        }
    }
}
