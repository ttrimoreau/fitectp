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
    }
}
