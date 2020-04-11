using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SoccerLeagueManager.Controllers
{
    public class SoccerMatchAPIController : BaseAPIController
    {
        private Repository.SoccerMatchRepository soccerMatchResource = new Repository.SoccerMatchRepository();
        public HttpResponseMessage Get()
        {
            return ToJson(soccerMatchResource.GetAllMatches().AsEnumerable());
        }
        public HttpResponseMessage Post([FromBody] Models.SoccerMatchModel value)
        {
            try
            {
                //apelam resursa care salveaza datele
                soccerMatchResource.InsertMatch(value);
                return ToJson(true);
            }
            catch
            {
                return ToJson(false);
            }
        }
        public HttpResponseMessage Put(Guid id, [FromBody] Models.SoccerMatchModel value)
        {
            try
            {
                //apelam resursa care salveaza datele
                soccerMatchResource.UpdateMatch(value);
                return ToJson(true);
            }
            catch
            {
                return ToJson(false);
            }
        }
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                //apelam resursa care sterge datele
                soccerMatchResource.DeleteMatch(id);
                return ToJson(true);
            }
            catch
            {
                return ToJson(true);
            }
        }
    }
}