using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SoccerLeagueManager.Controllers
{
    public class PlayerAPIController : BaseAPIController
    {
        private Repository.PlayerRepository playerResource = new Repository.PlayerRepository();
        public HttpResponseMessage Get()
        {
            return ToJson(playerResource.GetAllPlayers().AsEnumerable());
        }
        public HttpResponseMessage Post([FromBody] Models.PlayerModel value)
        {
            try
            {
                //apelam resursa care salveaza datele
                playerResource.InsertPlayer(value);
                return ToJson(true);
            }
            catch
            {
                return ToJson(false);
            }
        }
        public HttpResponseMessage Put(Guid id, [FromBody] Models.PlayerModel value)
        {
            try
            {
                //apelam resursa care salveaza datele
                playerResource.UpdatePlayer(value);
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
                playerResource.DeletePlayer(id);
                return ToJson(true);
            }
            catch
            {
                return ToJson(true);
            }
        }
    }
}