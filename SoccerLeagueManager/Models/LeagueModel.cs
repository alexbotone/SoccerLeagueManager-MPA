using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoccerLeagueManager.Models
{
    public class LeagueModel
    {
        public Guid IDLeague { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(50, ErrorMessage = "Name too long (max. 50 chars")]
        public string NameLeague { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(50, ErrorMessage = "Country too long (max. 50 chars")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        public int NumberOfTeams { get; set; }

        [StringLength(50, ErrorMessage = "Sponsor too long (max. 50 chars")]
        public string Sponsor { get; set; }
    }
}