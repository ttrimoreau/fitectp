using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.BusinessLayer;
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

        public Student CreateStudent(string lastname, string firstname, string username, string password, string email)
        {
            var student = new Student()
            {
                LastName = lastname,
                FirstMidName = firstname,
                UserName = username,
                Password = Authentication.SaltAndHash(password),
                Email = email,
                EnrollmentDate = DateTime.Now
            };

            this.dbContext.Students.Add(student);
            this.dbContext.SaveChanges();
            return student;
        }

        public Instructor CreateInstructor(string lastname, string firstname, string username, string password, string email)
        {
            var instructor = new Instructor()
            {
                LastName = lastname,
                FirstMidName = firstname,
                UserName = username,
                Password = Authentication.SaltAndHash(password),
                Email = email,
                HireDate = DateTime.Now
            };

            this.dbContext.Instructors.Add(instructor);
            this.dbContext.SaveChanges();
            return instructor;
        }

    }
}
