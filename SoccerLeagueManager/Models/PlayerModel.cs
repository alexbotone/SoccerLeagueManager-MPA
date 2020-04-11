using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoccerLeagueManager.Models
{
    public class PlayerModel
    {
        public Guid IDPlayer { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(50, ErrorMessage = "Name too long (max. 50 chars")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(50, ErrorMessage = "Surname too long (max. 50 chars")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(50, ErrorMessage = "Position too long (max. 50 chars")]
        public string Position { get; set; }

        [StringLength(50, ErrorMessage = "Team too long (max. 50 chars")]
        public string Team { get; set; }

        public int Value_EUR { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        public Guid IdTeam { get; set; }

    }
}