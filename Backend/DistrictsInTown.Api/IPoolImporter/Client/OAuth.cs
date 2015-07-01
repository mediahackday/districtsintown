using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OAuth;
using RestSharp;

namespace IPoolImporter.Client
{
    class OAuth
    {
        internal static RestRequest AddAuth(RestRequest request)
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
            string sig = oAuth.GenerateSignature(new Uri(uri), Program.API_KEY, Program.API_SECRET, null, null, "GET", timeStamp, nonce, out normalizedUrl, out normalizedRequestParameters);
            sig = HttpUtility.UrlEncode(sig);

            request.Method = Method.GET;
            string authString = String.Format(@"OAuth oauth_consumer_key=""{0}"", oauth_nonce=""{1}"", oauth_signature=""{2}"", oauth_signature_method=""HMAC-SHA1"", oauth_timestamp=""{3}"", oauth_version=""1.0""", Program.API_KEY, nonce, sig, timeStamp);
 
            request.AddHeader("Authorization", authString);

            return request;
        }

    }
}
