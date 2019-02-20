using ContosoUniversity.Controllers;
using ContosoUniversity.Controllers.api;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using ContosoUniversity.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;


namespace ContosoUniversity.Tests.Controllers
{
    class StudentsControllerTests : IntegrationTestsBase
    {
        //private MockHttpContextWrapper httpContext;
        private StudentsController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            //httpContext = new MockHttpContextWrapper();
            controllerToTest = new StudentsController();
            //controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        [Test]
        public void APIStudent_Success()
        {
            int testID = 100;
            string testLastName = "Petrolia";
            string testFirstName = "Matoula";
            DateTime testEnrollmentDate = DateTime.Now;
            Enrollment testEnrollment1 = new Enrollment()
            {
                EnrollmentID = 1,
                StudentID = testID,
                CourseID = 100
            };
            Enrollment testEnrollment2 = new Enrollment()
            {
                EnrollmentID = 2,
                StudentID = testID,
                CourseID = 200
            };
            List<Enrollment> testEnrollments = new List<Enrollment>();
            testEnrollments.Add(testEnrollment1);
            testEnrollments.Add(testEnrollment2);

            EntityGenerator generator = new EntityGenerator(dbContext);
            Student studentTest = generator.CreateStudentFull(testID, testLastName, testFirstName, testEnrollmentDate, testEnrollments);
            //Student savedStudent = dbContext.Students.Find(studentTest.ID);

            //Create a list of enrollments - only Course ID
            List<EnrollmentApiVM> testEnrollmentsAPI = new List<EnrollmentApiVM>();
            foreach (Enrollment enrollment in studentTest.Enrollments)
            {
                EnrollmentApiVM enrollmentApiVM = new EnrollmentApiVM();
                enrollmentApiVM.CourseId = enrollment.CourseID;
                testEnrollmentsAPI.Add(enrollmentApiVM);
            }

            IHttpActionResult okResult = controllerToTest.GetStudent(studentTest.ID);
            var contentResult = okResult as OkNegotiatedContentResult<StudentApiVM>;
            
            
            string expectedResult = "{\"id\":" + studentTest.ID + ",\"lastname\":" + studentTest.LastName + ",\"firstname\":" + studentTest.FirstMidName + 
                ",\"enrollmentDate\":" + studentTest.EnrollmentDate.ToString("yyyy-MM-dd") + ",\"enrollments\":[{\"CourseId\":" + testEnrollmentsAPI[0].CourseId +
                "},{\"CourseId\":" + testEnrollmentsAPI[1].CourseId + "}]}";


            Assert.IsNotNull(contentResult);
            Assert.AreEqual(studentTest.LastName, contentResult.Content.lastname);
            Assert.AreEqual(studentTest.FirstMidName, contentResult.Content.firstname);
            Assert.AreEqual(studentTest.ID, contentResult.Content.id);
            Assert.AreEqual(studentTest.EnrollmentDate.ToString("yyyy-MM-dd"), contentResult.Content.enrollmentDate);

            Assert.IsNotNull(contentResult.Content.enrollments);
            Assert.IsNotEmpty(contentResult.Content.enrollments);
            Assert.AreEqual(testEnrollmentsAPI[0], contentResult.Content.enrollments[0]);
            Assert.AreEqual(testEnrollmentsAPI[1], contentResult.Content.enrollments[1]);
            Assert.AreEqual(testEnrollmentsAPI, contentResult.Content.enrollments);
            
            //Assert.IsInstanceOf<OkResult>(okResult);
            //Assert.AreEqual(expectedResult, stringResult);


            //type of file, json or xml?

            //result type ok
            //compare strings
        }

        [Test]
        public void APIStudent_Fail_nonExistantID()
        {
            //404
            //null?
            //action result?
        }

        [Test]
        public void APIStudent_NoEnrollments()
        {
            //liste vide
        }

        [Test]
        public void APIStudent_Fail_instructorID()
        {
            //404
            //null?
            //action result?
        }

    }
}
