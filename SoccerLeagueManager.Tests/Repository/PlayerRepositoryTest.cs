using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoccerLeagueManager.Models;
using SoccerLeagueManager.Models.DBObjects;
using SoccerLeagueManager.Repository;
using SoccerLeagueManager.Tests.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerLeagueManager.Tests.Repository
{
    [TestClass]
    class PlayerRepositoryTest
    {

        private SoccerLeagueManager.Models.DBObjects.SoccerDataContext dbContext;
        private string testDbConnectionString;
         private PlayerRepository playerRepository;

         [TestInitialize]
         public void Initialize()
        { 
            //initializare setup-ului de test - conexiunea cu db-ul de test
            testDbConnectionString = ConfigurationManager.ConnectionStrings["SoccerLeagueManager.Tests.Properties.Settings.soccerConnectionString"].ConnectionString;

            dbContext = new SoccerLeagueManager.Models.DBObjects.SoccerDataContext(testDbConnectionString);
            playerRepository = new PlayerRepository(dbContext);
        }
        [TestCleanup]
         public void TestCleanup()
         {
             //curatarea recordurilor din db dupa rularea testului
             dbContext.Players.DeleteAllOnSubmit(dbContext.Players);
             dbContext.SubmitChanges();
         }

         [TestMethod]
         public void GetAllMembers_TwoRecordsExists()
         {
             //Arrange
             Player player1 = new Player
             {
                 IDPlayer = Guid.NewGuid(),
                 Name = "name",
                 Surname = "name1",
                 Position = "phd1",
                 Team = "M",
                 Value_EUR = 1234567,
                 IdTeam = Guid.NewGuid()
             };

             Player player2 = new Player
             {
                 IDPlayer = Guid.NewGuid(),
                 Name = "namesaS",
                 Surname = "surname1",
                 Position = "FDSD",
                 Team = "qwwe",
                 Value_EUR = 876543,
                 IdTeam = Guid.NewGuid()
             };

             List<PlayerModel> result = playerRepository.GetAllPlayers();
             //Assert
             Assert.AreEqual(2, result.Count);
             AssertUtility.AssertAreEqual(player1, result[0]);
             AssertUtility.AssertAreEqual(player2, result[1]);

         }

         [TestMethod]
         public void GetPacientByID_PacientExists()
         {

             Guid ID = Guid.NewGuid();
             Player expectedMember = new Player
             {
                 IDPlayer = ID,
                 Name = "namesdddaS",
                 Surname = "surnamedddddddd1",
                 Position = "FDSDddd",
                 Team = "qwwedddd",
                 Value_EUR = 876543,
                 IdTeam = Guid.NewGuid()
             };

             Player player2 = new Player
             {
                 IDPlayer = Guid.NewGuid(),
                 Name = "namesaS",
                 Surname = "wwwwsurname1",
                 Position = "sasasDSD",
                 Team = "qwwe",
                 Value_EUR = 4544444,
                 IdTeam = Guid.NewGuid()
             };

             dbContext.Players.InsertOnSubmit(expectedMember);
             dbContext.Players.InsertOnSubmit(player2);
             dbContext.SubmitChanges();
             //Act
             PlayerModel result = playerRepository.GetPlayerByID(ID);
             //Assert
             Assert.IsNotNull(result);
             Assert.AreEqual(ID, result.IDPlayer);
             AssertUtility.AssertAreEqual(expectedMember, result);
         }

         [TestMethod]
         public void InsertMember()
         {
             //Arrange
             PlayerModel playerModel = new PlayerModel
             {
                 IDPlayer = Guid.NewGuid(),
                 Name = "Vlad",
                 Surname = "POP",
                 Position = "FDSD",
                 Team = "qwwe",
                 Value_EUR = 876543,
                 IdTeam = Guid.NewGuid()
             };
             //Act
             playerRepository.InsertPlayer(playerModel);
             //Assert
             Player dbMember = dbContext.Players.FirstOrDefault(x => x.IDPlayer == playerModel.IDPlayer);
             Assert.IsNotNull(dbMember);
         }

         [TestMethod]
         public void DeleteMember_RecordExists()
         {
             //Arrange
             Guid ID = Guid.NewGuid();
             Player expectedMember = new Player
             {
                 IDPlayer = Guid.NewGuid(),
                 Name = "Sorin ION",
                 Surname = "Cataran",
                 Position = "FDSD",
                 Team = "qwwe",
                 Value_EUR = 333333,
                 IdTeam = Guid.NewGuid()
             };
             Player player2 = new Player
             {
                 IDPlayer = Guid.NewGuid(),
                 Name = "ION",
                 Surname = "andrei",
                 Position = "atacant",
                 Team = "Bilbao",
                 Value_EUR = 876543,
                 IdTeam = Guid.NewGuid()
             };
             dbContext.Players.InsertOnSubmit(expectedMember);
             dbContext.Players.InsertOnSubmit(player2);
             dbContext.SubmitChanges();
             //Act
             playerRepository.DeletePlayer(ID);
             //Assert
             Player dbMember = dbContext.Players.FirstOrDefault(x => x.IDPlayer == ID);
             Assert.IsNull(dbMember);
             Player dbMemberNotUpdated = dbContext.Players.FirstOrDefault(x => x.IDPlayer == player2.IDPlayer);
             Assert.IsNotNull(dbMemberNotUpdated);
         }
        }
}
