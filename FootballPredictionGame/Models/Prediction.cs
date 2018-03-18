using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictionGame.Models
{
    public class Prediction
    {
        public int PredictionId { get; set; }
        public int PredictorId { get; set; }

        public string HomeTeamId { get; set; }
        public string AwayTeamId { get; set; }
        public DateTime GameDate { get; set; }

        public int HomeResult { get; set; }
        public int AwayResult { get; set; }

        public virtual Predictor Predictor { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }
    }
}