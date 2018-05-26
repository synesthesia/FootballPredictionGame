using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballPredictionGame.Models
{
    public class Fixture
    {
        public int FixtureId { get; set; }

        [ForeignKey("HomeTeam")]
        public int HomeTeamId { get; set; }
        [ForeignKey("AwayTeam")]
        public int AwayTeamId { get; set; }    
        public DateTime GameDate { get; set; }

        public int HomeResult { get; set; }
        public int AwayResult { get; set; }

        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }
        public virtual ICollection<Prediction> Predictions { get; set; }

    }
}