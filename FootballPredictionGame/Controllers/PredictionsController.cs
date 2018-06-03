using FootballPredictionGame.DAL;
using FootballPredictionGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballPredictionGame.Controllers
{
    public class PredictionsController : Controller
    {
        private PredictionContext db = new PredictionContext();

        // GET: Predictions
        public ActionResult Index()
        {
            var predictions = db.Predictions.Where(p => p.PredictorId == 3).ToList();

            return View(predictions);
        }

        public JsonResult ChangeUser(Prediction model)
        {
            // Update model to your db
            string message = "Success";
            return Json(message, JsonRequestBehavior.AllowGet);
        }



    }
}