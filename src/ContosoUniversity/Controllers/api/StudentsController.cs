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
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System.Web.Routing;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers.api
{
    public class StudentsController : ApiController
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        // GET: api/Students
        public IQueryable<Student> GetPeople()
        {
            return db.Students;
        }

        // GET: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult GetStudent(int id)
        {
            if(db.Instructors.Any(x => x.ID == id))
            {
                return NotFound();
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            //List<Enrollment> enrollments = db.Enrollments.Where(s => s.StudentID == id).ToList();
            

            List<EnrollmentApiVM> CourseIdList = new List<EnrollmentApiVM>();

            StudentApiVM studentApiVM = new StudentApiVM();

            foreach(Enrollment enrollment in student.Enrollments)
            {
                EnrollmentApiVM enrollmentApiVM = new EnrollmentApiVM();
                enrollmentApiVM.CourseId = enrollment.CourseID;
                CourseIdList.Add(enrollmentApiVM);
            }

            studentApiVM.id = student.ID;
            studentApiVM.lastname = student.LastName;
            studentApiVM.firstname = student.FirstMidName;
            studentApiVM.enrollmentDate = student.EnrollmentDate.ToString("yyyy-MM-dd");
            studentApiVM.enrollments = CourseIdList;

            //return ViewModel
            return Ok(studentApiVM);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return db.People.Count(e => e.ID == id) > 0;
        }
    }
}