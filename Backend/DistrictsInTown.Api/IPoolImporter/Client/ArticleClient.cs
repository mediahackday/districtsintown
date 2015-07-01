using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAuth;
using RestSharp;
using System.Web;

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

        public void GetNumArticlesForDistrict(string district)
        {
            RestClient client = new RestClient(Program.BASEURL);
            var request = new RestRequest("search");
            
            request.AddParameter("q", "Mord");
            request.AddParameter("category", district);
            AddAuth(request);
            var response = client.Execute(request);
            
        }

        private RestRequest AddAuth(RestRequest request)
        {
            OAuthBase oAuth = new OAuthBase();

            string uri = Program.BASEURL + request.Resource;
            if (request.Parameters.Any())
            {
                string parameters = String.Join("&", request.Parameters.Select(p => p.Name + "=" + p.Value).ToArray());
                uri = uri + "?" + parameters;
                request.Resource = request.Resource + "?" + parameters;
                request.Parameters.Clear();
            }
            string nonce = "2nw9PgKWgzXgUsLlubDBa4tpVA6v00XE";
            nonce = oAuth.GenerateNonce();
            string timeStamp = "1435755041";
            timeStamp = oAuth.GenerateTimeStamp();
            string normalizedUrl;
            string normalizedRequestParameters;
            string sig = oAuth.GenerateSignature(new Uri(uri), _apiKey, _apiSecret, null, null, "GET", timeStamp, nonce, out normalizedUrl, out normalizedRequestParameters);
            sig = HttpUtility.UrlEncode(sig);
            //sig = "BOhB16IYbBN2jy%2FxviDxzaQJeqQ%3D";

            
           
            request.Method = Method.GET;
            string authString = String.Format(@"OAuth oauth_consumer_key=""{0}"", oauth_nonce=""{1}"", oauth_signature=""{2}"", oauth_signature_method=""HMAC-SHA1"", oauth_timestamp=""{3}"", oauth_version=""1.0""", _apiKey, nonce, sig, timeStamp);
            //authString = @"OAuth oauth_consumer_key=""mediahackday"", oauth_nonce=""2nw9PgKWgzXgUsLlubDBa4tpVA6v00XE"", oauth_signature=""018gBoweiBKvrCmbXqO62Oqp8b8%3D"", oauth_signature_method=""HMAC-SHA1"", oauth_timestamp=""1435755041"", oauth_version=""1.0""";
            // request.AddParameter("api_key", consumerKey);
            //request.AddParameter("oauth_consumer_key", _apiKey);
            //request.AddParameter("oauth_nonce", nonce);
            //request.AddParameter("oauth_timestamp", timeStamp);
            //request.AddParameter("oauth_signature_method", "HMAC-SHA1");
            //request.AddParameter("oauth_version", "1.0");
            //request.AddParameter("oauth_signature", sig);
            request.AddHeader("Authorization", authString);

            return request;
        }
    }
}
