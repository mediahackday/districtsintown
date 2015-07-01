using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace IPoolImporter.Client
{
    class CategoryClient
    {
        public void DumpCategories()
        {
            Dictionary<string, int> categories = new Dictionary<string, int>();
            IRestResponse<List<Category>> response;
            RestClient client = new RestClient(Program.BASEURL);
            var request = new RestRequest("categories");
            request.AddParameter("limit", 1000000);
            OAuth.AddAuth(request);
            response = client.Execute<List<Category>>(request);
            var sb = new StringBuilder();

            foreach (var cat in response.Data)
            {
                categories[cat.id] = cat.count;
            }
            foreach (var kv in categories.OrderBy(k => k.Value))
            {
                sb.Append(kv.Key);
                sb.Append(": ");
                sb.Append(kv.Value);
                sb.AppendLine();
            }
            File.AppendAllText("C:\\Temp\\categories2.txt", sb.ToString());
        }

        private class CategoryList
        {
            public List<Category> Categories { get; set; } 
        }
        private class Category
        {
            public string id { get; set; }
            public int count { get; set; }
        }
    }


}
