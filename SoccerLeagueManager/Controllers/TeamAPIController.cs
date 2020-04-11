using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SoccerLeagueManager.Controllers
{
    public class TeamAPIController : BaseAPIController
    {
        private Repository.TeamRepository teamResource = new Repository.TeamRepository();
        public HttpResponseMessage Get()
        {
            return ToJson(teamResource.GetAllTeams().AsEnumerable());
        }
        public HttpResponseMessage Post([FromBody] Models.TeamModel value)
        {
            try
            {
                //apelam resursa care salveaza datele
                teamResource.InsertTeam(value);
                return ToJson(true);
            }
            catch
            {
                return ToJson(false);
            }
        }
        public HttpResponseMessage Put(Guid id, [FromBody] Models.TeamModel value)
        {
            try
            {
                //apelam resursa care salveaza datele
                teamResource.UpdateTeam(value);
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
                teamResource.DeleteTeam(id);
                return ToJson(true);
            }
            catch
            {
                return ToJson(true);
            }
        }
    }
}