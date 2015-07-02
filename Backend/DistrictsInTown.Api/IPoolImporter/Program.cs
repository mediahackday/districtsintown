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
            { "Mitte", "POINT (13.366666666667 52.516666666667)" },
		    { "friedrichshain", "POINT (13.402222222222 52.5)" },
		    { "Friedrichshain-Kreuzberg", "POINT (13.402222222222 52.5)" },
		    { "kreuzberg", "POINT (13.402222222222 52.5)" },
		    { "Pankow", "POINT (13.402222222222 52.568888888889)" },
		    { "charlottenburg", "POINT ( 13.285 52.498888888889)" },
		    { "OTS_Charlottenburg-Wilmersdorf", "POINT ( 13.285 52.498888888889)" },
		    { "Charlottenburg-Wilmersdorf", "POINT ( 13.285 52.498888888889)" },
		    { "Spandau", "POINT (13.166666666667 52.533333333333)" },
		    { "spandau", "POINT (13.166666666667 52.533333333333)" },
		    { "steglitz", "POINT (13.25 52.433333333333)" },
		    { "zehlendorf", "POINT (13.25 52.433333333333)" },
		    { "Steglitz-Zehlendorf", "POINT (13.25 52.433333333333)" },
		    { "tempelhof", "POINT ( 13.383333333333 52.466666666667)" },
		    { "Tempelhof-SchÃ¶neberg", "POINT ( 13.383333333333 52.466666666667)" },
		    { "Tempelhof-Schöneberg", "POINT ( 13.383333333333 52.466666666667)" },
		    { "NeukÃ¶lln", "POINT ( 13.45 52.483333333333)" },
		    { "neukolln", "POINT ( 13.45 52.483333333333)" },
		    { "treptow", "POINT ( 13.566666666667 52.45)" },
		    { "Treptow-Köpenick", "POINT ( 13.566666666667 52.45)" },
		    { "OTS_Treptow-KÃ¶penick", "POINT ( 13.566666666667 52.45)" },
		    { "penick", "POINT ( 13.566666666667 52.45)" },
		    { "köpenick", "POINT ( 13.566666666667 52.45)" },
		    { "kopenick", "POINT ( 13.566666666667 52.45)" },
		    { "Treptow-KÃ¶penick", "POINT ( 13.566666666667 52.45)" },
		    { "marzahn", "POINT (13.584166666667 52.539722222222 )" },
		    { "Marzahn-Hellersdorf", "POINT (13.584166666667 52.539722222222 )" },
		    { "hellersdorf", "POINT (13.584166666667 52.539722222222 )" },
		    { "Lichtenberg", "POINT ( 13.55 2.533333333333)" },
		    { "lichtenberg", "POINT ( 13.55 2.533333333333)" },
		    { "Reinickendorf", "POINT ( 13.35 52.566666666667)" },
 		    { "reinickendorf", "POINT ( 13.35 52.566666666667)" },
       };

        internal static readonly Dictionary<string, string> PLZ = new Dictionary<string, string>()
        {
            { "POINT (13.366666666667 52.516666666667)", "10115" },
		    { "POINT (13.402222222222 52.5)", "10245" },
		    { "POINT (13.402222222222 52.568888888889)", "13129" },
		    { "POINT ( 13.285 52.498888888889)", "10625" },
		    { "POINT (13.166666666667 52.533333333333)", "13587" },
		    { "POINT (13.25 52.433333333333)", "12169" },
		    { "POINT ( 13.383333333333 52.466666666667)", "12101" },
		    { "POINT ( 13.45 52.483333333333)", "12051" },
		    { "POINT ( 13.566666666667 52.45)", "12435" },
		    { "POINT (13.584166666667 52.539722222222 )", "12685" },
		    { "POINT ( 13.55 2.533333333333)", "10365" },
		    { "POINT ( 13.35 52.566666666667)", "13507" },
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
            var multiplier = 50/(max - min);
            return nulledScore*multiplier;
        }
    }
}
