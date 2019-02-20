using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using ContosoUniversity.ViewModels;
using Moq;
using NUnit.Framework;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity.Tests.Controllers
{
    public class AuthenticationControllerTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private AuthenticationController controllerToTest;
        private SchoolContext dbContext;

        #region Initialisation ViewModels
        private readonly RegisterVM RegisterInstructor = new RegisterVM()
        {
            FirstMidName = "PrenomInstructeur",
            LastName = "NomInstructeur",
            UserName = "instructeur",
            Password = "instructeur",
            ConfirmPassword = "instructeur",
            Role = Role.Instructor
        };

        private readonly RegisterVM LoginInstructor = new RegisterVM()
        {
            UserName = "instructeur",
            Password = "instructeur"
        };
        private readonly RegisterVM RegisterStudent = new RegisterVM()
        {
            FirstMidName = "PrenomStudent",
            LastName = "NomStudent",
            UserName = "student",
            Password = "student",
            ConfirmPassword = "student",
            Role = Role.Instructor
        };

        private readonly RegisterVM LoginStudent = new RegisterVM()
        {
            UserName = "student",
            Password = "student"
        }; 
        #endregion

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new AuthenticationController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        [TearDown]
        public void RemoveNewDatabase()
        {
            SettingUpTests();
        }

        [Test]
        public void Register_RegisterStudent_ViewLogin()
        {
            RedirectToRouteResult result = controllerToTest.Register(RegisterStudent) as RedirectToRouteResult;
            //Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Register_RegisterInstructor_ViewLogin()
        {
            RedirectToRouteResult result = controllerToTest.Register(RegisterInstructor) as RedirectToRouteResult;
            //Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Login_LoginStudent_ViewHomeIndex()
        {
            RedirectToRouteResult result = controllerToTest.Login(LoginStudent) as RedirectToRouteResult;
            //Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Login_LoginInstructor_ViewHomeIndex()
        {
            RedirectToRouteResult result = controllerToTest.Register(LoginInstructor) as RedirectToRouteResult;
            //Assert.That(result, Is.Not.Null);
        }
    }
}
