using SoccerLeagueManager.Mappers;
using SoccerLeagueManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerLeagueManager.Repository
{
    public class SoccerMatchRepository
    {
        //injectam containerul ORM
        private Models.DBObjects.SoccerDataContext dbContext;

        public SoccerMatchRepository()
        {
            this.dbContext = new Models.DBObjects.SoccerDataContext();
        }

        //injectam un dbContext pt a face repository testabil
        public SoccerMatchRepository(Models.DBObjects.SoccerDataContext dbContext)
        {
            this.dbContext = dbContext;
        }//todo

        public List<SoccerMatchModel> GetAllMatches()
        {
            List<SoccerMatchModel> MatchList = new List<SoccerMatchModel>();

            foreach (Models.DBObjects.SoccerMatch player in dbContext.SoccerMatches)
            {
                MatchList.Add(player.MapObject<SoccerMatchModel>());
            }

            return MatchList;
        }

        public SoccerMatchModel GetMatchByID(Guid ID)
        {
            Models.DBObjects.SoccerMatch existingMatch = dbContext.SoccerMatches.FirstOrDefault(x => x.IDMatch == ID);

            if (existingMatch != null)
                return existingMatch.MapObject<SoccerMatchModel>();
            else
                return null;
        }

        public void InsertMatch(SoccerMatchModel match)
        {
            match.IDMatch = Guid.NewGuid(); //generate new id
                                            //  dbContext.Matches.InsertOnSubmit(MapModelToDbObject(match));


            dbContext.SoccerMatches.InsertOnSubmit(match.MapObject<Models.DBObjects.SoccerMatch>());


            //add to ORM layer
            dbContext.SubmitChanges(); //commit to db
        }

        public void UpdateMatch(SoccerMatchModel matchModel)
        {
            //get existing record to update
            Models.DBObjects.SoccerMatch existingMatch = dbContext.SoccerMatches.FirstOrDefault(x => x.IDMatch == matchModel.IDMatch);
            if (existingMatch != null)
            {
                //map updated values with keeping the ORM objecte reference
                existingMatch.IDMatch = matchModel.IDMatch;
                existingMatch.HomeTeam = matchModel.HomeTeam;
                existingMatch.GuestTeam = matchModel.GuestTeam;
                existingMatch.Stadium = matchModel.Stadium;
                existingMatch.City = matchModel.City;
                existingMatch.IdLeague = matchModel.IdLeague;
                existingMatch.UpdateObject(matchModel);

                dbContext.SubmitChanges(); //commit to db
            }
        }

        public void DeleteMatch(Guid ID)
        {
            //get existing record to delete
            Models.DBObjects.SoccerMatch matchToDelete = dbContext.SoccerMatches.FirstOrDefault(x => x.IDMatch == ID);
            if (matchToDelete != null)
            {
                dbContext.SoccerMatches.DeleteOnSubmit(matchToDelete); //mark for deletion
                dbContext.SubmitChanges();
            }
        }

        //map ORM model to Model object – mapper method
        private SoccerMatchModel MapDbObjectToModel(Models.DBObjects.SoccerMatch dbMatch)
        {
            SoccerMatchModel matchModel = new SoccerMatchModel();
            if (dbMatch != null)
            {
                matchModel.IDMatch = dbMatch.IDMatch;
                matchModel.HomeTeam = dbMatch.HomeTeam;
                matchModel.GuestTeam = dbMatch.GuestTeam;
                matchModel.Stadium = dbMatch.Stadium;
                matchModel.City = dbMatch.City;
                matchModel.IdLeague = dbMatch.IdLeague;
                return matchModel;
            }
            return null;
        }
        //map Model object to ORM model – mapper method
        private Models.DBObjects.SoccerMatch MapModelToDbObject(SoccerMatchModel matchModel)
        {
            Models.DBObjects.SoccerMatch dbMatchModel = new Models.DBObjects.SoccerMatch();
            if (matchModel != null)
            {
                dbMatchModel.IDMatch = matchModel.IDMatch;
                dbMatchModel.HomeTeam = matchModel.HomeTeam;
                dbMatchModel.GuestTeam = matchModel.GuestTeam;
                dbMatchModel.Stadium = matchModel.Stadium;
                dbMatchModel.City = matchModel.City;
                dbMatchModel.IdLeague = matchModel.IdLeague;
                return dbMatchModel;
            }
            return null;
        }
    }
}