using System.Net;
using System.Net.Http;
using System.Web.Http;
using DistrictsInTown.Api.Models;
using DistrictsInTown.Api.Repositories;
using Newtonsoft.Json;

namespace DistrictsInTown.Api.Controllers
{
    public class LocationController : ApiController
    {
        // GET: api/Location/value
        public HttpResponseMessage Get(string value)
        {
            var repository = new LocationRepository();
            var data = new Data
            {
                Locations = repository.Get(value),
                Max = 10
            };

            var json = JsonConvert.SerializeObject(data);

            return new HttpResponseMessage
            {
                Content = new StringContent(json),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}