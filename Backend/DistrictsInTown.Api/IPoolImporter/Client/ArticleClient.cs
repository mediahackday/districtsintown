using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace IPoolImporter.Client
{
    class ArticleClient
    {
        private readonly string _apiKey;
        private readonly string _apiSecret;

        public ArticleClient(string apiKey, string apiSecret)
        {
            _apiKey = apiKey;
            _apiSecret = apiSecret;
        }

        public GetArticlesForDistrict(string district)
        {
            RestClient client = new RestClient(Program.BASEURL);


        }
    }
}
