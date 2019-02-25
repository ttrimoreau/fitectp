using ContosoUniversity.BusinessLayer;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using ContosoUniversity.ViewModels.ApiVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ContosoUniversity.Controllers.api
{
    public class AgendaStudentController : ApiController
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudentAgenda(int id)
        {
            // find instead of any: any searches the database, find searches the context
            if (db.People.Find(id) is Instructor)
            {
                return NotFound();
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }


            List<EnrollmentApiVM> CourseIdList = new List<EnrollmentApiVM>();

            StudentAgendaApiVM studentAgendaApiVM = new StudentAgendaApiVM();

            Dictionary<int,LessonApiVM> lessonsList = new Dictionary<int, LessonApiVM>();
            int key = 0;
            foreach (Enrollment enrollment in student.Enrollments)
            {
                EnrollmentApiVM enrollmentApiVM = new EnrollmentApiVM();
                

                enrollmentApiVM.courseId = enrollment.CourseID; 
                List<Lessons> lessons= new List<Lessons>();
                lessons = db.Lessons.Where(c => c.CourseID == enrollment.CourseID).ToList();
                foreach (var item in lessons)
                {
                    LessonApiVM lessonApiVM = new LessonApiVM
                    {
                        courseId = item.CourseID,
                        day = item.Day.ToString(),
                        duration = item.Duration.ToString(),
                        startHour = item.HourStart.ToString()

                    };
                    lessonsList.Add(key,lessonApiVM);
                    key++;
                }
                CourseIdList.Add(enrollmentApiVM);
            }

            studentAgendaApiVM.id = student.ID;
            studentAgendaApiVM.lastname = student.LastName;
            studentAgendaApiVM.firstname = student.FirstMidName;
            studentAgendaApiVM.lessonsVMs =lessonsList;
            studentAgendaApiVM.enrollments = CourseIdList;

            //return ViewModel
            return Ok(studentAgendaApiVM);
        }
    }
}
