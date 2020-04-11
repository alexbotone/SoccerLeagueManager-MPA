using SoccerLeagueManager.Mappers;
using SoccerLeagueManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerLeagueManager.Repository
{
    public class LeagueRepository
    {
        //injectam containerul ORM
        private Models.DBObjects.SoccerDataContext dbContext;

        public LeagueRepository()
        {
            this.dbContext = new Models.DBObjects.SoccerDataContext();
        }

        //injectam un dbContext pt a face repository testabil
        public LeagueRepository(Models.DBObjects.SoccerDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<LeagueModel> GetAllLeagues()
        {
            List<LeagueModel> LeagueList = new List<LeagueModel>();

            foreach (Models.DBObjects.League league in dbContext.Leagues)
            {
                LeagueList.Add(league.MapObject<LeagueModel>());
            }

            return LeagueList;
        }

        public LeagueModel GetLeagueByID(Guid ID)
        {
            Models.DBObjects.League existingLeague = dbContext.Leagues.FirstOrDefault(x => x.IDLeague == ID);

            if (existingLeague != null)
                return existingLeague.MapObject<LeagueModel>();
            else
                return null;
        }

        public void InsertLeague(LeagueModel league)
        {
            league.IDLeague = Guid.NewGuid(); //generate new id
                                              //  dbContext.Leagues.InsertOnSubmit(MapModelToDbObject(leagueModel));


            dbContext.Leagues.InsertOnSubmit(league.MapObject<Models.DBObjects.League>());


            //add to ORM layer
            dbContext.SubmitChanges(); //commit to db
        }

        public void UpdateLeague(LeagueModel leagueModel)
        {
            //get existing record to update
            Models.DBObjects.League existingLeague = dbContext.Leagues.FirstOrDefault(x => x.IDLeague == leagueModel.IDLeague);
            if (existingLeague != null)
            {
                //map updated values with keeping the ORM objecte reference
                existingLeague.IDLeague = leagueModel.IDLeague;
                existingLeague.NameLeague = leagueModel.NameLeague;
                existingLeague.Country = leagueModel.Country;
                existingLeague.NumberOfTeams = leagueModel.NumberOfTeams;
                existingLeague.Sponsor = leagueModel.Sponsor;

                existingLeague.UpdateObject(leagueModel);


                dbContext.SubmitChanges(); //commit to db
            }
        }

        public void DeleteLeague(Guid ID)
        {
            //get existing record to delete
            Models.DBObjects.League leagueToDelete = dbContext.Leagues.FirstOrDefault(x => x.IDLeague == ID);
            if (leagueToDelete != null)
            {
                dbContext.Leagues.DeleteOnSubmit(leagueToDelete); //mark for deletion
                dbContext.SubmitChanges();
            }
        }

        //map ORM model to Model object – mapper method
        private LeagueModel MapDbObjectToModel(Models.DBObjects.League dbLeague)
        {
            LeagueModel leagueModel = new LeagueModel();
            if (dbLeague != null)
            {
                leagueModel.IDLeague = dbLeague.IDLeague;
                leagueModel.NameLeague = dbLeague.NameLeague;
                leagueModel.Country = dbLeague.Country;
                leagueModel.NumberOfTeams = dbLeague.NumberOfTeams;
                leagueModel.Sponsor = dbLeague.Sponsor;
                return leagueModel;
            }
            return null;
        }
        //map Model object to ORM model – mapper method
        private Models.DBObjects.League MapModelToDbObject(LeagueModel leagueModel)
        {
            Models.DBObjects.League dbLeagueModel = new Models.DBObjects.League();
            if (leagueModel != null)
            {
                dbLeagueModel.IDLeague = leagueModel.IDLeague;
                dbLeagueModel.NameLeague = leagueModel.NameLeague;
                dbLeagueModel.Country = leagueModel.Country;
                dbLeagueModel.NumberOfTeams = leagueModel.NumberOfTeams;
                dbLeagueModel.Sponsor = leagueModel.Sponsor;
                return dbLeagueModel;
            }
            return null;
        }

    }
}