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
        public Course createCourse(int instructorId)
        {
            var course = new Course()
            {
                DepartmentID

            }
        }
        #endregion
    }
}
