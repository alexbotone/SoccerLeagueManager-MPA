using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoccerLeagueManager.Models
{
    public class SoccerMatchModel
    {
        public Guid IDMatch { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(50, ErrorMessage = "Home Team too long (max. 50 chars")]
        public string HomeTeam { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(50, ErrorMessage = "Guest Team too long (max. 50 chars")]
        public string GuestTeam { get; set; }

        [StringLength(50, ErrorMessage = "Stadium too long (max. 50 chars")]
        public string Stadium { get; set; }

        [StringLength(50, ErrorMessage = "City too long (max. 50 chars")]
        public string City { get; set; }

        [Required(ErrorMessage = "Mandatory Field")]
        public Guid IdLeague { get; set; }
    }
}