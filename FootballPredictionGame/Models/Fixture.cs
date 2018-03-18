using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictionGame.Models
{
    public class Fixture
    {
        public int FixtureId { get; set; }
        public string HomeTeamId { get; set; }
        public string AwayTeamId { get; set; }    
        public DateTime GameDate { get; set; }

        public int HomeResult { get; set; }
        public int AwayResult { get; set; }

        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }
        public virtual ICollection<Prediction> Predictions { get; set; }

    }
}