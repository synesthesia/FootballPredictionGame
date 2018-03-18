using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictionGame.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }

        public virtual ICollection<Fixture> HomeMatches { get; set; }
        public virtual ICollection<Fixture> AwayMatches { get; set; }
    }
}