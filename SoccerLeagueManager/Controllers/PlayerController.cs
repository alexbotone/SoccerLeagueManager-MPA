using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoccerLeagueManager.Controllers
{
    public class PlayerController : Controller
    {

        private Repository.PlayerRepository playerRepository = new Repository.PlayerRepository();

        [AllowAnonymous]
        // GET: Player
        public ActionResult Index()
        {
            List<Models.PlayerModel> players = playerRepository.GetAllPlayers();

            return View("Index",players);
        }

        [AllowAnonymous]
        // GET: Player/Details/5
        public ActionResult Details(Guid id)
        {
            Models.PlayerModel playerModel = playerRepository.GetPlayerByID(id);

            return View("Details",playerModel);
        }

        [Authorize(Roles = "User, Admin")]
        // GET: Player/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        [Authorize(Roles = "User, Admin")]
        // POST: Player/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //instantiem modelul
                Models.PlayerModel playerModel = new Models.PlayerModel();

                //incarcam datele in model
                UpdateModel(playerModel);

                if (User.Identity.IsAuthenticated)
                {
                    playerModel.Team = User.Identity.Name + " - add team " + playerModel.Team;
                    playerModel.Position = User.Identity.Name + " - add position " + playerModel.Position;
                }

                //apelam sursa sa salveze datele
                playerRepository.InsertPlayer(playerModel);

                //redirectare catre index in caz de succes
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        [Authorize(Roles = "User, Admin")]
        // GET: Player/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.PlayerModel playerModel = playerRepository.GetPlayerByID(id);

            return View("Edit",playerModel);
        }

        [Authorize(Roles = "User, Admin")]
        // POST: Player/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                //instantiem modelul
                Models.PlayerModel playerModel = new Models.PlayerModel();

                //incarcare date din model
                UpdateModel(playerModel);

                //apelam resursa care salveaza datele
                playerRepository.UpdatePlayer(playerModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: Player/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.PlayerModel playerModel = playerRepository.GetPlayerByID(id);

            return View("Delete",playerModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: Player/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                playerRepository.DeletePlayer(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
