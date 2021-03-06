﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPredictionGame.Models
{
    public class Predictor
    {
        public int PredictorId { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Points { get; set; }

        public virtual ICollection<Prediction> Predictions { get; set; }
    }
}