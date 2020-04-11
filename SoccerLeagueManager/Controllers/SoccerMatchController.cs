using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoccerLeagueManager.Controllers
{
    public class SoccerMatchController : Controller
    {

        private Repository.SoccerMatchRepository soccerMatchRepository = new Repository.SoccerMatchRepository();

        [AllowAnonymous]
        // GET: SoccerMatch
        public ActionResult Index()
        {
            List<Models.SoccerMatchModel> soccerMatches = soccerMatchRepository.GetAllMatches();

            return View("Index", soccerMatches);
        }

        [AllowAnonymous]
        // GET: SoccerMatch/Details/5
        public ActionResult Details(Guid id)
        {
            Models.SoccerMatchModel soccerMatchModel = soccerMatchRepository.GetMatchByID(id);

            return View("Details", soccerMatchModel);
        }

        [Authorize(Roles = "User, Admin")]
        // GET: SoccerMatch/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        [Authorize(Roles = "User, Admin")]
        // POST: SoccerMatch/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //instantiem modelul
                Models.SoccerMatchModel soccerMatchModel = new Models.SoccerMatchModel();

                //incarcam datele in model
                UpdateModel(soccerMatchModel);

                if (User.Identity.IsAuthenticated)
                {
                    soccerMatchModel.Stadium = User.Identity.Name + " - add stadium " + soccerMatchModel.Stadium;
                    soccerMatchModel.GuestTeam = User.Identity.Name + " - add guest team " + soccerMatchModel.GuestTeam;
                    soccerMatchModel.HomeTeam = User.Identity.Name + " - add home team " + soccerMatchModel.HomeTeam;
                }

                //apelam sursa sa salveze datele
                soccerMatchRepository.InsertMatch(soccerMatchModel);

                //redirectare catre index in caz de succes
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        [Authorize(Roles = "User, Admin")]
        // GET: SoccerMatch/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.SoccerMatchModel soccerMatchModel = soccerMatchRepository.GetMatchByID(id);

            return View("Edit", soccerMatchModel);
        }

        [Authorize(Roles = "User, Admin")]
        // POST: SoccerMatch/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                //instantiem modelul
                Models.SoccerMatchModel soccerMatchModel = new Models.SoccerMatchModel();

                //incarcare date din model
                UpdateModel(soccerMatchModel);

                //apelam resursa care salveaza datele
                soccerMatchRepository.UpdateMatch(soccerMatchModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: SoccerMatch/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.SoccerMatchModel soccerMatchModel = soccerMatchRepository.GetMatchByID(id);

            return View("Delete", soccerMatchModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: SoccerMatch/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                soccerMatchRepository.DeleteMatch(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
