using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SoccerLeagueManager.Controllers
{
    public class BaseAPIController : ApiController
    {
        protected HttpResponseMessage ToJson(dynamic obj)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj), System.Text.Encoding.UTF8, "application/json");
            return response;
        }

    }
}
