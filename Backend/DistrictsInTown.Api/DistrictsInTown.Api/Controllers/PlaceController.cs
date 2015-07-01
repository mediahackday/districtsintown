using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DistrictsInTown.Api.Models;
using DistrictsInTown.Api.Repositories;
using Newtonsoft.Json;

namespace DistrictsInTown.Api.Controllers
{
    public class PlaceController : ApiController
    {
        // GET: api/Location/value
        public HttpResponseMessage Get(string value)
        {
            try
            {
                var repository = new PlaceRepository();
                var data = new Data
                {
                    Places = repository.Get(value)
                };
                data.Max = data.Places.Count();

                var json = JsonConvert.SerializeObject(data);

                return new HttpResponseMessage
                {
                    Content = new StringContent(json),
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception)
            {
                return new HttpResponseMessage {StatusCode = HttpStatusCode.InternalServerError};
            }
        }
    }
}