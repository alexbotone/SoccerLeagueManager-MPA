using SoccerLeagueManager.Mappers;
using SoccerLeagueManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerLeagueManager.Repository
{
    public class PlayerRepository
    {

        //injectam containerul ORM
        private Models.DBObjects.SoccerDataContext dbContext;

        public PlayerRepository()
        {
            this.dbContext = new Models.DBObjects.SoccerDataContext();
        }

        //injectam un dbContext pt a face repository testabil
        public PlayerRepository(Models.DBObjects.SoccerDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<PlayerModel> GetAllPlayers()
        {
            List<PlayerModel> PlayerList = new List<PlayerModel>();

            foreach (Models.DBObjects.Player player in dbContext.Players)
            {
                PlayerList.Add(player.MapObject<PlayerModel>());
            }

            return PlayerList;
        }

        public PlayerModel GetPlayerByID(Guid ID)
        {
            Models.DBObjects.Player existingPlayer = dbContext.Players.FirstOrDefault(x => x.IDPlayer == ID);

            if (existingPlayer != null)
                return existingPlayer.MapObject<PlayerModel>();
            else
                return null;
        }

        public void InsertPlayer(PlayerModel player)
        {
            player.IDPlayer = Guid.NewGuid(); //generate new id
                                          // dbContext.Players.InsertOnSubmit(MapModelToDbObject(player));


            dbContext.Players.InsertOnSubmit(player.MapObject<Models.DBObjects.Player>());


            //add to ORM layer
            dbContext.SubmitChanges(); //commit to db
        }

        public void UpdatePlayer(PlayerModel playerModel)
        {
            //get existing record to update
            Models.DBObjects.Player existingPlayer = dbContext.Players.FirstOrDefault(x => x.IDPlayer == playerModel.IDPlayer);
            if (existingPlayer != null)
            {
                //map updated values with keeping the ORM objecte reference
                existingPlayer.IDPlayer = playerModel.IDPlayer;
                existingPlayer.Name = playerModel.Name;
                existingPlayer.Surname = playerModel.Surname;
                existingPlayer.Position = playerModel.Position;
                existingPlayer.Team = playerModel.Team;
                existingPlayer.Value_EUR = playerModel.Value_EUR;
                existingPlayer.IdTeam = playerModel.IdTeam;
                existingPlayer.UpdateObject(playerModel);

                dbContext.SubmitChanges(); //commit to db
            }
        }

        public void DeletePlayer(Guid ID)
        {
            //get existing record to delete
            Models.DBObjects.Player playerToDelete = dbContext.Players.FirstOrDefault(x => x.IDPlayer == ID);
            if (playerToDelete != null)
            {
                dbContext.Players.DeleteOnSubmit(playerToDelete); //mark for deletion
                dbContext.SubmitChanges();
            }
        }

        //map ORM model to Model object – mapper method
        private PlayerModel MapDbObjectToModel(Models.DBObjects.Player dbPlayer)
        {
            PlayerModel playerModel = new PlayerModel();
            if (dbPlayer != null)
            {
                playerModel.IDPlayer = dbPlayer.IDPlayer;
                playerModel.Name = dbPlayer.Name;
                playerModel.Surname = dbPlayer.Surname;
                playerModel.Position = dbPlayer.Position;
                playerModel.Team = dbPlayer.Team;
                playerModel.Value_EUR = dbPlayer.Value_EUR.Value;
                playerModel.IdTeam = dbPlayer.IdTeam;
                return playerModel;
            }
            return null;
        }
        //map Model object to ORM model – mapper method
        private Models.DBObjects.Player MapModelToDbObject(PlayerModel playerModel)
        {
            Models.DBObjects.Player dbPlayerModel = new Models.DBObjects.Player();
            if (playerModel != null)
            {
                dbPlayerModel.IDPlayer = playerModel.IDPlayer;
                dbPlayerModel.Name = playerModel.Name;
                dbPlayerModel.Surname = playerModel.Surname;
                dbPlayerModel.Position = playerModel.Position;
                dbPlayerModel.Team = playerModel.Team;
                dbPlayerModel.Value_EUR = playerModel.Value_EUR;
                dbPlayerModel.IdTeam = playerModel.IdTeam;
                return dbPlayerModel;
            }
            return null;
        }
    }
}