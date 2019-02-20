using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class LessonsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Lessons
        public ActionResult Index()
        {
            return View(db.Lessons.ToList());
        }

        // GET: Lessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            return View(lessons);
        }

        // GET: Lessons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lessons/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Day,HourStart,Duration")] Lessons lessons)
        {
            if (ModelState.IsValid)
            {
                db.Lessons.Add(lessons);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lessons);
        }

        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            return View(lessons);
        }

        // POST: Lessons/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Day,HourStart,Duration")] Lessons lessons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessons).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lessons);
        }

        // GET: Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            return View(lessons);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lessons lessons = db.Lessons.Find(id);
            db.Lessons.Remove(lessons);
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
