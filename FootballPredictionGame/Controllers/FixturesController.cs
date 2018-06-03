using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FootballPredictionGame.DAL;
using FootballPredictionGame.Models;

namespace FootballPredictionGame.Controllers
{
    public class FixturesController : Controller
    {
        private PredictionContext db = new PredictionContext();

        // GET: Fixtures
        public ActionResult Index(DateTime? SelectedDate)
        {
            var fixtureDates = db.Fixtures.OrderBy(q => q.GameDate)
                .ToList().Select(q => q.GameDate.Date.ToString("dd MMM yyyy")).Distinct()
                .Select(q => new SelectListItem { Value = q, Text = q })
                .ToList();

            ViewBag.SelectedDate = fixtureDates;
            DateTime fixDate = SelectedDate.GetValueOrDefault();

            IQueryable<Fixture> fixtures = db.Fixtures.Include(h => h.HomeTeam)
                                                      .Include(h => h.AwayTeam);
            var sql = fixtures.ToString();
            var fixtureList = fixtures.ToList().Where(c => !SelectedDate.HasValue || c.GameDate.Date == fixDate);
            return View(fixtureList);
        }

        // GET: Fixtures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fixture fixture = db.Fixtures.Find(id);
            if (fixture == null)
            {
                return HttpNotFound();
            }
            return View(fixture);
        }

        // GET: Fixtures/Create
        public ActionResult Create()
        {
            PopulateHomeDropDownList();
            PopulateAwayDropDownList();
            return View();
        }

        // POST: Fixtures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FixtureId,HomeTeamId,AwayTeamId,GameDate,HomeResult,AwayResult")] Fixture fixture)
        {
            if (ModelState.IsValid)
            {
                db.Fixtures.Add(fixture);
                db.SaveChanges();

                var predictors = db.Predictors.ToList();

                foreach (var predictor in predictors)
                {
                    db.Predictions.Add(new Prediction {PredictorId = predictor.PredictorId, FixtureId = fixture.FixtureId });
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(fixture);
        }

        // GET: Fixtures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fixture fixture = db.Fixtures.Find(id);
            if (fixture == null)
            {
                return HttpNotFound();
            }

            PopulateHomeDropDownList(fixture.HomeTeamId);
            PopulateAwayDropDownList(fixture.AwayTeamId);
            return View(fixture);
        }

        // POST: Fixtures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FixtureId,HomeTeamId,AwayTeamId,GameDate,HomeResult,AwayResult")] Fixture fixture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fixture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(fixture);
        }

        private void PopulateHomeDropDownList(object selectedTeam = null)
        {
            var teamsQuery = from d in db.Teams
                                   orderby d.Name
                                   select d;
            ViewBag.HomeTeamId = new SelectList(teamsQuery, "TeamId", "Name", selectedTeam);
        }

        private void PopulateAwayDropDownList(object selectedTeam = null)
        {
            var teamsQuery = from d in db.Teams
                             orderby d.Name
                             select d;
            ViewBag.AwayTeamId = new SelectList(teamsQuery, "TeamId", "Name", selectedTeam);
        }

        // GET: Fixtures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fixture fixture = db.Fixtures.Find(id);
            if (fixture == null)
            {
                return HttpNotFound();
            }
            return View(fixture);
        }

        // POST: Fixtures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Prediction> predictions = db.Predictions.Where(p => p.FixtureId == id).ToList();

            foreach (var prediction in predictions)
            {
                db.Predictions.Remove(prediction);
            }
            db.SaveChanges();

            Fixture fixture = db.Fixtures.Find(id);
            db.Fixtures.Remove(fixture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
