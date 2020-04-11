using SoccerLeagueManager.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerLeagueManager.ViewModels
{
    public class TeamViewModel
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Position { get; set; }

        public List<Team> CodeSnippets = new List<Team>();
    }
}