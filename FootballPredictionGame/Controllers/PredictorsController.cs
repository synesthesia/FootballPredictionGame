using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FootballPredictionGame.DAL;
using FootballPredictionGame.Models;

namespace FootballPredictionGame.Controllers
{
    public class PredictorsController : Controller
    {
        private PredictionContext db = new PredictionContext();

        // GET: Predictors
        public ActionResult Index()
        {
            return View(db.Predictors.ToList());
        }

        // GET: Predictors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predictor predictor = db.Predictors.Find(id);
            if (predictor == null)
            {
                return HttpNotFound();
            }
            return View(predictor);
        }

        // GET: Predictors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Predictors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PredictorId,Email,LastName,FirstName,Points")] Predictor predictor)
        {
            if (ModelState.IsValid)
            {
                db.Predictors.Add(predictor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(predictor);
        }

        // GET: Predictors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predictor predictor = db.Predictors.Find(id);
            if (predictor == null)
            {
                return HttpNotFound();
            }
            return View(predictor);
        }

        // POST: Predictors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PredictorId,Email,LastName,FirstName,Points")] Predictor predictor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(predictor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(predictor);
        }

        // GET: Predictors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predictor predictor = db.Predictors.Find(id);
            if (predictor == null)
            {
                return HttpNotFound();
            }
            return View(predictor);
        }

        // POST: Predictors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Predictor predictor = db.Predictors.Find(id);
            db.Predictors.Remove(predictor);
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
