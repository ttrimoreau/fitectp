using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.BusinessLayer;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers
{
    public class LessonsController : Controller
    {


        private SchoolContext db = new SchoolContext();
        LessonsBusiness lessonB = new LessonsBusiness();
  private void DropDownList(object selectedCourse = null)
        {
            List<Course> courseQuery = lessonB.CourseList();
            ViewBag.CourseID = new SelectList(courseQuery, "CourseID", "Title", selectedCourse);
        }
        
        // GET: Lessons

        public ActionResult Index(int? SelectedCourse)
        {

            List<Course> courses = lessonB.CourseList();
            ViewBag.SelectedCourse = new SelectList(courses, "CourseID", "Title", SelectedCourse);
            int courseID = SelectedCourse.GetValueOrDefault();

            return View(lessonB.ListeFiltreLesson(SelectedCourse, courseID).ToList());
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
            DropDownList();
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Title");
            TempData["instructorId"] = Session[SessionMessage.UserID];
            return View();
        }



        // POST: Lessons/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lessons lessons)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    lessons.InstructorID = (int)TempData["InstructorID"];
                    lessonB.AddLesson(lessons);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
    
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            DropDownList(lessons.ID);
                return View(lessons);    
        }

        // GET: Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Lessons lessons = lessonB.FindLesson(id);

            if (lessons == null)
            {
                return HttpNotFound();
            }

            DropDownList(lessons.ID);
            return View(lessons);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Lessons lessons = lessonB.FindLesson(id);
        

                try
                {
                db.SaveChanges();

                return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
        
            DropDownList(lessons.ID);
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
