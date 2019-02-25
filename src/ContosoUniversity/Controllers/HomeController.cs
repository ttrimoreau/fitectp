using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.BusinessLayer;
using ContosoUniversity.DAL;
using ContosoUniversity.Enum;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;


namespace ContosoUniversity.Controllers
{
    [AuthorizedRoleFilter(Role = "Instructor", Roles ="Student")]
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Session[SessionMessage.UserID]!=null)
            {
                if (Session[SessionMessage.UserRole].ToString()=="Student")
                {
                    return View("StudentIndex");
                }
                else if (Session[SessionMessage.UserRole].ToString() == "Instructor")
                {
                    int id = (int)Session[SessionMessage.UserID];
                    Dictionary<int, Dictionary<Day, string>> agenda = new Dictionary<int, Dictionary<Day, string>>();
                    for (int hour = 8; hour <= 19; hour++)
                    {
                        Dictionary<Day, string> HourDay = new Dictionary<Day, string>();
                        foreach (Day day in (Day[])System.Enum.GetValues(typeof(Day)))
                        {
                            string libelle = "";
                            Lessons lesson = db.Lessons.Where(l => (l.InstructorID == id && l.Day == day))
                                .Where(l => (l.HourStart.Hour == hour || l.HourStart.Hour < hour && (l.HourStart.Hour+(l.Duration/60) > hour)))
                                .FirstOrDefault();
                            if (lesson != null)
                            {
                                libelle = lesson.Course.Title;
                            }
                            HourDay.Add(day, libelle);
                        }
                        agenda.Add(hour, HourDay);
                    }
                    ViewBag.Lessons = agenda;
                    return View("InstructorHome");
                }
                
            }
            ViewBag.Courses = db.Courses.ToList();
            return View();
        }

        public ActionResult About()
        {
            // Commenting out LINQ to show how to do the same thing in SQL.
            //IQueryable<EnrollmentDateGroup> = from student in db.Students
            //           group student by student.EnrollmentDate into dateGroup
            //           select new EnrollmentDateGroup()
            //           {
            //               EnrollmentDate = dateGroup.Key,
            //               StudentCount = dateGroup.Count()
            //           };

            // SQL version of the above LINQ code.
            string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                + "FROM Person "
                + "WHERE Discriminator = 'Student' "
                + "GROUP BY EnrollmentDate";
            IEnumerable<EnrollmentDateGroup> data = db.Database.SqlQuery<EnrollmentDateGroup>(query);

            return View(data.ToList());
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}