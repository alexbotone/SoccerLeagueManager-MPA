using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoccerLeagueManager.Models
{
    public class TeamModel
    {
        public Guid IDTeam { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(50, ErrorMessage = "Name too long (max. 150 chars")]
        public string  Name { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(50, ErrorMessage = "City too long (max. 50 chars")]
        public string City { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        public Guid IdLeague { get; set; }
    }
}