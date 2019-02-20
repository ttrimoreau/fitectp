using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using ContosoUniversity.ViewModels;
using Moq;
using NUnit.Framework;
using System;
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
            PersonRole = Role.Instructor,
            Email = "Email@Instructeur.com",
            HireDate = DateTime.Now
        };

        private readonly LoginVM LoginInstructor = new LoginVM()
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
            PersonRole = Role.Student,
            Email = "Email@Student.com",
            HireDate = DateTime.Now
        };

        private readonly LoginVM LoginStudent = new LoginVM()
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Login"));
        }

        [Test]
        public void Register_RegisterInstructor_ViewLogin()
        {
            RedirectToRouteResult result = controllerToTest.Register(RegisterInstructor) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Login"));
        }

        [Test]
        public void Login_LoginStudent_ViewHomeIndex()
        {
            RedirectToRouteResult result = controllerToTest.Login(LoginStudent) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Login_LoginInstructor_ViewHomeIndex()
        {
            RedirectToRouteResult result = controllerToTest.Login(LoginInstructor) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }
    }
}
