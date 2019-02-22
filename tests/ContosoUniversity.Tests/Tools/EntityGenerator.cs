using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Tests.Tools
{
    public class EntityGenerator
    {
        private readonly SchoolContext dbContext;

        public EntityGenerator(SchoolContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Student CreateStudent(string lastname, string firstname)
        {
            var student = new Student()
            {
                LastName = lastname,
                FirstMidName = firstname
            };

            this.dbContext.Students.Add(student);
            return student;
        }

        public Student CreateStudentForUploadImage(string lastname, string firstname)
        {
            var student = new Student()
            {
                LastName = lastname,
                FirstMidName = firstname,
                EnrollmentDate =DateTime.Now
            };

            this.dbContext.Students.Add(student);
            return student;
        }

        public Instructor CreateInstructor(string lastname, string firstname)
        {
            var instructor = new Instructor()
            {
                LastName = lastname,
                FirstMidName = firstname
            };
            this.dbContext.Instructors.Add(instructor);
            return instructor;
        }

        #region Course
        //public Course createCourse(int instructorId)
        //{
        //    var course = new Course()
        //    {
        //        DepartmentID

        //    };
        //}
        #endregion

        public Student CreateStudentUser(string login, string password)
        {
            Student student = new Student()
            {
                FirstMidName = "firstmidname",
                LastName = "lastname",
                EnrollmentDate = DateTime.Now,
                Email = "email@address.com",
                UserName = login,
                Password = password,
                ID = 25
            };

            this.dbContext.Students.Add(student);
            this.dbContext.SaveChanges();
            return student;
        }

        //New generator for students, used for student API tests
        public Student CreateStudentFull(int id, string lastname, string firstname, DateTime enrollmentDate, List<Enrollment> enrollments)
        {
            var student = new Student()
            {
                ID = id,
                LastName = lastname,
                FirstMidName = firstname,
                EnrollmentDate = enrollmentDate,
                Enrollments = enrollments
            };

            this.dbContext.Students.Add(student);
            return student;
        }

    
        // Instructor Generator
        //public Instructor CreateInstructor(string lastname, string firstname)
        //{
        //    var instructor = new Instructor()
        //    {
        //        LastName = lastname,
        //        FirstMidName = firstname
        //    };

        //    this.dbContext.Instructors.Add(instructor);
        //    return instructor;
        //}

    }
}
