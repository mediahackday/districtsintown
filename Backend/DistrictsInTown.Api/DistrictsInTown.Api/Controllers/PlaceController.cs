﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DistrictsInTown.Api.Activities;
using DistrictsInTown.Api.Models;
using DistrictsInTown.Api.Repositories;
using Newtonsoft.Json;

namespace DistrictsInTown.Api.Controllers
{
    public class PlaceController : ApiController
    {
        // GET: api/Place?keyword=park&keyword=coffee...
        public HttpResponseMessage Get([FromUri] IList<string> keyword)
        {
            try
            {
                var repository = new PlaceRepository();
                var calculateScores = new CalculateScores();

                var calculatedPlaces = calculateScores.For(repository.Get(keyword));

                var data = new Data
                {
                    Places = calculatedPlaces,
                    Max = 10
                };

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