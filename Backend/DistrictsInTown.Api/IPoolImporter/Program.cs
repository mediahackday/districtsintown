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
        internal static readonly string[] BEZIRKE = 
        {
            "friedrichshain",
            "Friedrichshain-Kreuzberg",
            "kreuzberg",
            "reinickendorf",
            "Reinickendorf",
            "Mitte",
            "charlottenburg",
            "OTS_Charlottenburg-Wilmersdorf",
            "Charlottenburg-Wilmersdorf",
            "steglitz",
            "Steglitz-Zehlendorf",
            "zehlendorf",
            "tempelhof",
            "Tempelhof-SchÃ¶neberg",
            "Tempelhof-Schöneberg",
            "spandau",
            "Spandau",
            "NeukÃ¶lln",
            "neukolln",
            "treptow",
            "Treptow-Köpenick",
            "OTS_Treptow-KÃ¶penick",
            "Treptow-KÃ¶penick",
            "penick",
            "köpenick",
            "kopenick",
            "marzahn",
            "Marzahn-Hellersdorf",
            "hellersdorf",
            "lichtenberg",
            "Lichtenberg",
        };
        

        static void Main(string[] args)
        {
            GetHistogram();
        }

        private static void GetHistogram()
        {
            var client = new ArticleClient();
            Dictionary<string, int> hist = new Dictionary<string, int>();
            foreach (var district in BEZIRKE)
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
            foreach (var district in BEZIRKE)
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
            foreach (var district in BEZIRKE)
            {
                client.GetNumArticlesForDistrict(district);
            }
            Console.ReadLine();
        }
    }
}
