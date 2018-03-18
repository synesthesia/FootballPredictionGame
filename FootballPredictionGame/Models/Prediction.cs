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
        public int FixtureId { get; set; }

        public int HomeResult { get; set; }
        public int AwayResult { get; set; }

        public virtual Predictor Predictor { get; set; }
        public virtual Fixture Fixture { get; set; }

    }
}