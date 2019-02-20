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
            //controllerToTestError
            //What is this line needed for?
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

            IHttpActionResult okResult = controllerToTest.GetStudent(100);
            var contentResult = okResult as OkNegotiatedContentResult<StudentApiVM>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(studentTest.LastName, contentResult.Content.Lastname);
            Assert.AreEqual(studentTest.FirstMidName, contentResult.Content.Firstname);
            Assert.AreEqual(studentTest.ID, contentResult.Content.id);
            Assert.AreEqual(studentTest.EnrollmentDate.ToString("yyyy-MM-dd"), contentResult.Content.EnrollmentDate);

            Assert.IsNotNull(contentResult.Content.enrollments);
            Assert.IsNotEmpty(contentResult.Content.enrollments);
            //Assert.AreEqual(testEnrollmentsAPI, contentResult.Content.enrollments);

            //type of file, json or xml?

            //action result? - verifier url?
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
        public void APIStudent_Fail_instructorID()
        {
            //404
            //null?
            //action result?
        }

    }
}
