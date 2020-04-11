using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoccerLeagueManager.Controllers
{
    public class TeamController : Controller
    {

        private Repository.TeamRepository teamRepository = new Repository.TeamRepository();

        [AllowAnonymous]
        // GET: Team
        public ActionResult Index()
        {
            List<Models.TeamModel> teams = teamRepository.GetAllTeams();

            return View("Index", teams);
        }

        [AllowAnonymous]
        // GET: Team/Details/5
        public ActionResult Details(Guid id)
        {
            Models.TeamModel teamModel = teamRepository.GetTeamByID(id);

            return View("Details", teamModel);
        }

        [Authorize(Roles = "User, Admin")]
        // GET: Team/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        [Authorize(Roles = "User, Admin")]
        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //instantiem modelul
                Models.TeamModel teamModel = new Models.TeamModel();

                //incarcam datele in model
                UpdateModel(teamModel);

                if (User.Identity.IsAuthenticated)
                {
                    teamModel.Email = User.Identity.Name + " - add email " + teamModel.Email;
                }

                //apelam sursa sa salveze datele
                teamRepository.InsertTeam(teamModel);

                //redirectare catre index in caz de succes
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Create");
            }
        }

        [Authorize(Roles = "User, Admin")]
        // GET: Team/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.TeamModel teamModel = teamRepository.GetTeamByID(id);

            return View("Edit",teamModel);
        }

        [Authorize(Roles = "User, Admin")]
        // POST: Team/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                //instantiem modelul
                Models.TeamModel teamModel = new Models.TeamModel();

                //incarcare date din model
                UpdateModel(teamModel);

                //apelam resursa care salveaza datele
                teamRepository.UpdateTeam(teamModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit");
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: Team/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.TeamModel teamModel = teamRepository.GetTeamByID(id);

            return View("Delete",teamModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: Team/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                teamRepository.DeleteTeam(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }
    }
}
