using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoccerLeagueManager.Controllers
{
    public class LeagueController : Controller
    {
        private Repository.LeagueRepository leagueRepository = new Repository.LeagueRepository();

        [AllowAnonymous]
        // GET: League
        public ActionResult Index()
        {
            List<Models.LeagueModel> leagues = leagueRepository.GetAllLeagues();

            return View("Index", leagues);
        }

        [AllowAnonymous]
        // GET: League/Details/5
        public ActionResult Details(Guid id)
        {
            Models.LeagueModel leagueModel = leagueRepository.GetLeagueByID(id);

            return View("Details",leagueModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: League/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        [Authorize(Roles = "Admin")]
        // POST: League/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //instantiem modelul
                Models.LeagueModel leagueModel = new Models.LeagueModel();

                //incarcam datele in model
                UpdateModel(leagueModel);

                if (User.Identity.IsAuthenticated)
                {
                    leagueModel.Country = User.Identity.Name + " - add country " + leagueModel.Country;
                }

                //apelam sursa sa salveze datele
                leagueRepository.InsertLeague(leagueModel);

                //redirectare catre index in caz de succes
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: League/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.LeagueModel leagueModel = leagueRepository.GetLeagueByID(id);

            return View("Edit",leagueModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: League/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                //instantiem modelul
                Models.LeagueModel leagueModel = new Models.LeagueModel();

                //incarcare date din model
                UpdateModel(leagueModel);

                //apelam resursa care salveaza datele
                leagueRepository.UpdateLeague(leagueModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: League/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.LeagueModel leagueModel = leagueRepository.GetLeagueByID(id);

            return View("Delete", leagueModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: League/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                leagueRepository.DeleteLeague(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
