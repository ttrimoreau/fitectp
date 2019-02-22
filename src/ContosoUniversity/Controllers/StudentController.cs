using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        // GET: Student
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:  // Name ascending 
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            SchoolContext db = new SchoolContext();
            if (Session["UserId"] == null)
            {
                TempData["ErrorMessage"] = " Vous n'êtes pas autorisés à accéder à la section Détail. Veuillez vous loggez.";
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            StudentDetailsVM model = new StudentDetailsVM();

            List<Course> CourseEnrolled = new List<Course>();
            foreach (Enrollment enrollment in student.Enrollments)
            {
                CourseEnrolled.Add(db.Courses.FirstOrDefault(c => c.CourseID == enrollment.CourseID));
            }

            List<int> CourseEnrolledID = CourseEnrolled.Select(c => c.CourseID).ToList();

            var temp = db.Courses.Where(c => !CourseEnrolledID.Contains(c.CourseID));

            List<Course> CoursesNotEnrolled = temp.ToList();

            List<EnrollmentVM> NotEnrolled = new List<EnrollmentVM>();

            //foreach (var item in CoursesNotEnrolled)
            //{
            //    EnrollmentVM enrollment = new EnrollmentVM
            //    {
            //        StudentID = (int)id,
            //        CourseID = item.CourseID,

            //    };
            //    NotEnrolled.Add(enrollment);
            //}
            model.EnrollmentDate = student.EnrollmentDate;
            model.Enrollments = student.Enrollments;
            model.Student = student;
            model.StudentID = student.ID;
            model.CoursesList = CoursesNotEnrolled;

            return View(model);
        }

        //Post
        [HttpPost]
        public ActionResult Details(StudentDetailsVM enrollmentVM)
        {
            SchoolContext db = new SchoolContext();
            if (Session["UserID"] == null)
            {
                return View();
            }
            int id = int.Parse(Session["UserId"].ToString());

            Enrollment enrollment = new Enrollment
            {
                StudentID = enrollmentVM.StudentID,
                CourseID = enrollmentVM.CourseID
            };

            db.Enrollments.Add(enrollment);
            db.SaveChanges();
            ViewBag.Message = "Subscription successful !";
            return RedirectToAction("Details", new { id = enrollment.StudentID });
        }

        [HttpPost]

        public ActionResult StudentEnrollment(string CourseID, int StudentID)
        {
            try
            {
                Enrollment enrollment = new Enrollment
                {
                    CourseID = Int32.Parse(CourseID),
                    StudentID = StudentID,

                };
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = enrollment.StudentID });
            }
            catch (Exception)
            {
                //a faire
                throw;
            }




        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstMidName, EnrollmentDate")]Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.Students.Find(id);
            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
            {
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
            }
            return View(studentToUpdate);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
