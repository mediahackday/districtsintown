using System;
using System.Collections.Generic;
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
        internal readonly string[] BEZIRKE = 
        {
            "friedrichshain",
            "kreuzberg",
            "reinickendorf",
            "mitte",
            "charlottenburg",
            "steglitz",
            "tempelhof"
        };

        static void Main(string[] args)
        {
            var client = new ArticleClient(API_KEY, API_SECRET);
            client.GetNumArticlesForDistrict("blubb");

            Console.ReadLine();
        }
    }
}
