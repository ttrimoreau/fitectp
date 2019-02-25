using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ContosoUniversity.BusinessLayer;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels.ApiVM;

namespace ContosoUniversity.Controllers.api
{
    public class InstructorsController : ApiController
    {
        private SchoolContext db = new SchoolContext();


        // GET: api/Instructors/5
        [ResponseType(typeof(Instructor))]
        public IHttpActionResult GetInstructor(int id, string weeklyschedule)
        {

            if (InstructorExists(id) == false)
            {
                return NotFound();
            }

            SchoolContext db = new SchoolContext();
            InstructorApiVM instructorApiVM = new InstructorApiVM();
            instructorApiVM.instructorId = id;
            instructorApiVM.schedule = new List<LessonApiVM>();

            List<Course> courseList = db.Courses.Where(x => x.Instructors.Any(y => y.ID == id)).ToList();


            //List<Lessons> lessonsListe = db.Lessons.Where(c => c.InstructorID == id).ToList();

            List<Lessons> lessonsListe = new List<Lessons>();
            List<Lessons> lessonsAll = db.Lessons.ToList();
            foreach (var course in courseList)
            {
                foreach (var lesson in lessonsAll.Where(l => l.CourseID == course.CourseID))
                {
                    LessonApiVM lessonApiVM = new LessonApiVM
                    {
                        courseId = lesson.CourseID,
                        day = lesson.Day.ToString(),
                        duration = lesson.Duration.ToString(),
                        startHour = lesson.HourStart.ToString("HH'h'mm")
                    };
                    instructorApiVM.schedule.Add(lessonApiVM);
                }


            }


            //foreach (Lessons item in lessonsListe)
            //{
            //    LessonApiVM lessonApiVM = new LessonApiVM
            //    {
            //        courseId = item.CourseID,
            //        day = item.Day.ToString(),
            //        duration = item.Duration.ToString(),
            //        startHour = item.HourStart.ToString("HH'h'mm")
            //    };
            //    instructorApiVM.schedule.Add(lessonApiVM);
            //}


            return Ok(instructorApiVM);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstructorExists(int id)
        {
            
            return db.Instructors.Count(e => e.ID == id) > 0;
        }
    }
}