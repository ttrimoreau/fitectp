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
            

            //Create a list of enrollments - only Course ID
            List<EnrollmentApiVM> testEnrollmentsAPI = new List<EnrollmentApiVM>();
            foreach (Enrollment enrollment in studentTest.Enrollments)
            {
                EnrollmentApiVM enrollmentApiVM = new EnrollmentApiVM();
                enrollmentApiVM.CourseId = enrollment.CourseID;
                testEnrollmentsAPI.Add(enrollmentApiVM);
            }

            IHttpActionResult okResult = controllerToTest.GetStudent(studentTest.ID);
            OkNegotiatedContentResult<StudentApiVM> contentResult = okResult as OkNegotiatedContentResult<StudentApiVM>;
            
            // string to test the format - not used
            //string expectedResult = "{\"id\":" + studentTest.ID + ",\"lastname\":" + studentTest.LastName + ",\"firstname\":" + studentTest.FirstMidName + 
            //    ",\"enrollmentDate\":" + studentTest.EnrollmentDate.ToString("yyyy-MM-dd") + ",\"enrollments\":[{\"CourseId\":" + testEnrollmentsAPI[0].CourseId +
            //    "},{\"CourseId\":" + testEnrollmentsAPI[1].CourseId + "}]}";


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
            
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<StudentApiVM>), okResult);




        }

        [Test]
        public void APIStudent_Fail_nonExistantID()
        {
            IHttpActionResult notOkResult = controllerToTest.GetStudent(-1);
            OkNegotiatedContentResult<StudentApiVM> contentResult = notOkResult as OkNegotiatedContentResult<StudentApiVM>;

            Assert.IsNull(contentResult);
            Assert.IsInstanceOf(typeof(NotFoundResult), notOkResult);


        }

        [Test]
        public void APIStudent_NoEnrollments()
        {
            int testID = 100;
            string testLastName = "Petrolia";
            string testFirstName = "Matoula";
            DateTime testEnrollmentDate = DateTime.Now;
            List<Enrollment> testEnrollments = new List<Enrollment>();


            EntityGenerator generator = new EntityGenerator(dbContext);
            Student studentTest = generator.CreateStudentFull(testID, testLastName, testFirstName, testEnrollmentDate, testEnrollments);


          

            IHttpActionResult okResult = controllerToTest.GetStudent(studentTest.ID);
            OkNegotiatedContentResult<StudentApiVM> contentResult = okResult as OkNegotiatedContentResult<StudentApiVM>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(studentTest.LastName, contentResult.Content.lastname);
            Assert.AreEqual(studentTest.FirstMidName, contentResult.Content.firstname);
            Assert.AreEqual(studentTest.ID, contentResult.Content.id);
            Assert.AreEqual(studentTest.EnrollmentDate.ToString("yyyy-MM-dd"), contentResult.Content.enrollmentDate);
            Assert.IsNotNull(contentResult.Content.enrollments);
            Assert.IsEmpty(contentResult.Content.enrollments);
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<StudentApiVM>), okResult);
 
        }



        [Test]
        public void APIStudent_Fail_instructorID()
        {
            string instrLastName = "Adjara";
            string instrFirstName = "Maria";

            EntityGenerator generator = new EntityGenerator(dbContext);
            Instructor InstructorTest = generator.CreateInstructor(instrLastName, instrFirstName);


            IHttpActionResult notOkResult = controllerToTest.GetStudent(InstructorTest.ID);
            OkNegotiatedContentResult<StudentApiVM> contentResult = notOkResult as OkNegotiatedContentResult<StudentApiVM>;

            Assert.IsNull(contentResult);
            Assert.IsInstanceOf(typeof(NotFoundResult), notOkResult);



        }

    }
}
