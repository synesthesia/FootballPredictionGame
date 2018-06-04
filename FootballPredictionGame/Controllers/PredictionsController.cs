using FootballPredictionGame.DAL;
using FootballPredictionGame.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            var fixtureDates = db.Fixtures.OrderBy(q => q.GameDate)
                .ToList().Select(q => q.GameDate.Date.ToString("dd MMM yyyy")).Distinct()
                .Select(q => new SelectListItem { Value = q, Text = q })
                .ToList();

            ViewBag.SelectedDate = fixtureDates;


            var predictions = db.Predictions.Where(p => p.PredictorId == 3)
                .Include(p => p.Predictor)
                .Include(p => p.Fixture)
                .ToList();

            return View(predictions);
        }

        public JsonResult ChangeUser(Prediction model)
        {
            // Update model to your db
            var prediction = db.Predictions.Where(p => p.PredictionId == model.PredictionId).SingleOrDefault();

            // input validation
            if (TryUpdateModel(prediction, "",
                        new string[] { "HomeResult", "AwayResult" }))
            {
                try
                {
                    db.Entry(prediction).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            string message = "Success";
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}