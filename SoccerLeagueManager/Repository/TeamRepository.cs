using SoccerLeagueManager.Mappers;
using SoccerLeagueManager.Models;
using SoccerLeagueManager.Models.DBObjects;
using SoccerLeagueManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerLeagueManager.Repository
{
    public class TeamRepository
    {
       
        
          //injectam containerul ORM
          private Models.DBObjects.SoccerDataContext dbContext;

          public TeamRepository()
          {
              this.dbContext = new Models.DBObjects.SoccerDataContext();
          }

          //injectam un dbContext pt a face repository testabil
          public TeamRepository(Models.DBObjects.SoccerDataContext dbContext)
          {
              this.dbContext = dbContext;
          }

          public List<TeamModel> GetAllTeams()
          {
              List<TeamModel> TeamList = new List<TeamModel>();

              foreach (Models.DBObjects.Team team in dbContext.Teams)
              {
                TeamList.Add(team.MapObject<TeamModel>());
              }

              return TeamList;
          }

          public TeamModel GetTeamByID(Guid ID)
          {
              Models.DBObjects.Team existingTeam = dbContext.Teams.FirstOrDefault(x => x.IDTeam == ID);

              if (existingTeam != null)
                  return existingTeam.MapObject<TeamModel>();
              else
                  return null;
          }

          public void InsertTeam(TeamModel team)
          {
              team.IDTeam = Guid.NewGuid(); //generate new id
                                                //  dbContext.Teams.InsertOnSubmit(MapModelToDbObject(teamModel));


              dbContext.Teams.InsertOnSubmit(team.MapObject<Models.DBObjects.Team>());


              //add to ORM layer
              dbContext.SubmitChanges(); //commit to db
          }

          public void UpdateTeam(TeamModel teamModel)
          {
              //get existing record to update
              Models.DBObjects.Team existingTeam = dbContext.Teams.FirstOrDefault(x => x.IDTeam == teamModel.IDTeam);
              if (existingTeam != null)
              {
                  //map updated values with keeping the ORM objecte reference
                  existingTeam.IDTeam = teamModel.IDTeam;
                  existingTeam.Name = teamModel.Name;
                  existingTeam.City = teamModel.City;
                  existingTeam.Email = teamModel.Email;
                  existingTeam.IdLeague = teamModel.IdLeague;
                  existingTeam.UpdateObject(teamModel);

                  dbContext.SubmitChanges(); //commit to db
              }
          }

          public void DeleteTeam(Guid ID)
          {
              //get existing record to delete
              Models.DBObjects.Team teamToDelete = dbContext.Teams.FirstOrDefault(x => x.IDTeam == ID);
              if (teamToDelete != null)
              {
                  dbContext.Teams.DeleteOnSubmit(teamToDelete); //mark for deletion
                  dbContext.SubmitChanges();
              }
          }
//       public TeamViewModel GetTeams(Guid memberID)
//       {
//           TeamViewModel teamsViewModel = new TeamViewModel();
//           Team team = soccerDataContext..FirstOrDefault(x =>
//          x.IDMember == memberID);
//           if (member != null)
//           {
//               memberCodesnippetsViewModel.Name = member.Name;
//               memberCodesnippetsViewModel.Position = member.Position;
//               memberCodesnippetsViewModel.Title = member.Title;
//
//               IQueryable<Team> memberTeams = SoccerDataContext.Team.Where(x => x.IDMember == memberID);
//               foreach (Team dbTeam in memberTeams)
//               {
//                   Models.Team codeSnippetModel = new Models.Team(
//                  );
//                   codeSnippetModel.Title = dbTeam.Title;
//                   codeSnippetModel.ContentCode = dbTeam.ContentCode;
//                   codeSnippetModel.Revision = dbTeam.Revision;
//                   teamsViewModel..Add(codeSnippetModel
//                  );
//               }
//           }
//           return teamsViewModel;
//       }


        //map ORM model to Model object – mapper method
        private TeamModel MapDbObjectToModel(Models.DBObjects.Team dbTeam)
          {
              TeamModel teamModel = new TeamModel();
              if (dbTeam != null)
              {
                  teamModel.IDTeam = dbTeam.IDTeam;
                  teamModel.Name = dbTeam.Name;
                  teamModel.City = dbTeam.City;
                  teamModel.Email = dbTeam.Email;
                  teamModel.IdLeague = dbTeam.IdLeague;
                  return teamModel;
              }
              return null;
          }
          //map Model object to ORM model – mapper method
          private Models.DBObjects.Team MapModelToDbObject(TeamModel teamModel)
          {
              Models.DBObjects.Team dbTeamModel = new Models.DBObjects.Team();
              if (teamModel != null)
              {
                  dbTeamModel.IDTeam = teamModel.IDTeam;
                  dbTeamModel.Name = teamModel.Name;
                  dbTeamModel.City = teamModel.City;
                  dbTeamModel.Email = teamModel.Email;
                  dbTeamModel.IdLeague = teamModel.IdLeague;
                  return dbTeamModel;
              }
              return null;
          }

        
    }
}
