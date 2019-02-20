using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using NUnit.Framework;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity.Tests.Controllers
{
    public class InstructorControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private InstructorController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new InstructorController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        [Test]
        public void GetDetails_ValidInstructor_Success()
        {
            string expectedLastName = "Dubois";
            string expectedFirstName = "George";

            EntityGenerator generator = new EntityGenerator(dbContext);
            Instructor instructor = generator.CreateInstructor(expectedLastName, expectedFirstName);

            var result = controllerToTest.Details(instructor.ID) as ViewResult;
            var resultModel = result.Model as Instructor;

            Assert.That(result, Is.Not.Null);
            Assert.That(resultModel, Is.Not.Null);
            Assert.That(expectedLastName, Is.EqualTo(resultModel.LastName));
            Assert.That(expectedFirstName, Is.EqualTo(resultModel.FirstMidName));
        }

        [Test]
        public void Edit_ValidInstructorData_Success()
        {
            string expectedLastName = "Wood";
            string previousLastName = "Dubois";
            string previousFirstName = "George";


            EntityGenerator generator = new EntityGenerator(dbContext);
            Instructor instructor = generator.CreateInstructor(previousLastName, previousFirstName);
            instructor.LastName = expectedLastName;

            FormDataHelper.PopulateFormData(controllerToTest, instructor);

            var result = controllerToTest.Edit(instructor.ID) as ViewResult;

            Instructor savedInstructor = dbContext.Instructors.Find(instructor.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That((result.Model as Instructor).LastName, Is.EqualTo(expectedLastName));
            Assert.That(savedInstructor.LastName, Is.EqualTo(expectedLastName));
        }
    }
}
