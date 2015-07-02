using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using OAuth;
using RestSharp;
using System.Web;

namespace IPoolImporter.Client
{
    class ArticleClient
    {
        private static readonly string[] keywords = {"Mord", "Überfall", "Unfall"};

        public void GetNumArticlesForDistrict(string district)
        {
        
            Dictionary<string, int> numResults = new Dictionary<string, int>();
            
            var response = GetResponseForDistrict(district, "");
            
            Console.Write(district + ": " + response.Data.pagination.total + ", ");
            foreach (var key in keywords)
            {
                var result = GetResponseForDistrict(district, key);
                Console.Write(key + ": " + result.Data.pagination.total + ", ");
            }
            Console.WriteLine();
        }

        public List<string> GetKeywordsForDistrict(string district)
        {
            var response = GetResponseForDistrict(district, "", 600);
            List<string> result =
                response.Data.documents.Where(d => d.entities != null && d.entities.keywords != null).SelectMany(d => d.entities.keywords.Select(k => k.lemma)).Distinct().ToList();
            return result;
        }

        public void GetHistogram(string district, Dictionary<string, int> hist)
        {
            var response = GetResponseForDistrict(district, "", 600);
            List<string> result =
                response.Data.documents.Where(d => d.entities != null && d.entities.keywords != null).SelectMany(d => d.entities.keywords.Select(k => k.lemma)).Distinct().ToList();
            foreach (string key in result)
            {
                if (hist.ContainsKey(key))
                {
                    hist[key] += 1;

                }
                else
                {
                    hist[key] = 1;
                }
            }
        }


        private static IRestResponse<Articles> GetResponseForDistrict(string district, string query, int limit = 1)
        {
            RestClient client = new RestClient(Program.BASEURL);
            var request = new RestRequest("search");

            request.AddParameter("category", "\"" + district + "\"");
            request.AddParameter("limit", limit);
            if (!String.IsNullOrEmpty(query))
            {
                request.AddParameter("q", query);
            }
            OAuth.AddAuth(request);
            var response = client.Execute<Articles>(request);
            return response;
        }

        private class Articles
        {
            public Pagination pagination { get; set; }
            public List<Document> documents { get; set; } 
        }

        private class Pagination
        {
            public int total { get; set; }
            public int offset { get; set; }
            public int limit { get; set; }
        }

        private class Document
        {
            public Entities entities { get; set; }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder("Document ");
                if (entities == null)
                {
                    sb.Append("no entities ");
                }
                else
                {
                    if (entities.keywords == null)
                    {
                        sb.Append("no keywords ");
                    }
                    else
                    {
                        sb.Append(" keywords: " + entities.keywords.Count);
                    }
                    if (entities.location == null)
                    {
                        sb.Append(" no location ");
                    }
                    else
                    {
                        sb.Append("location" + entities.location);
                    }
                }
                return sb.ToString();
            }
            
        }

        private class Entities
        {
            public List<KeyWord> keywords { get; set; }
            public string location { get; set; }
            
        }

        private class KeyWord
        {
            public string lemma { get; set; }
            public decimal weight { get; set; }
        }

    }
}
