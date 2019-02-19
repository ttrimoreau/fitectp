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
    class StudentsControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private StudentsController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new StudentsController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        [Test]
        public void APIStudent_Success()
        {
            //type of file, json or xml?
            //not null
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
