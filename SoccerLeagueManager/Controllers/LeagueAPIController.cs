using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SoccerLeagueManager.Controllers
{
    public class LeagueAPIController : BaseAPIController
    {
        private Repository.LeagueRepository leagueResource = new Repository.LeagueRepository();
        public HttpResponseMessage Get()
        {
            return ToJson(leagueResource.GetAllLeagues().AsEnumerable());
        }
        public HttpResponseMessage Post([FromBody] Models.LeagueModel value)
        {
            try
            {
                //apelam resursa care salveaza datele
                leagueResource.InsertLeague(value);
                return ToJson(true);
            }
            catch
            {
                return ToJson(false);
            }
        }
        public HttpResponseMessage Put(Guid id, [FromBody] Models.LeagueModel value)
        {
            try
            {
                //apelam resursa care salveaza datele
                leagueResource.UpdateLeague(value);
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
                leagueResource.DeleteLeague(id);
                return ToJson(true);
            }
            catch
            {
                return ToJson(true);
            }
        }
    }
}