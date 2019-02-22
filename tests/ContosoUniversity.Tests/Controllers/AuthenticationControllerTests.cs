using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.BusinessLayer;
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

        #region Constants
        private readonly string REGISTER_ERROR_MSG = "This UserName is already taken";
        private readonly string VALIDATION_ERROR_MSG = "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.";

        
        #endregion

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

        private readonly RegisterVM RegisterVM_Student_JohnDoe = new RegisterVM()
        {
            FirstMidName = "John",
            LastName = "Doe",
            UserName = "StudentJohnDoe",
            Password = "JohnDoe2000",
            ConfirmPassword = "JohnDoe2000",
            PersonRole = Role.Student,
            Email = "JohnDoeStudent@aol.com",
            HireDate = DateTime.Now
        };

        private readonly RegisterVM RegisterVM_Instructor_JohnDoe = new RegisterVM()
        {
            FirstMidName = "John",
            LastName = "Doe",
            UserName = "InstructorJohnDoe",
            Password = "JohnDoe33",
            ConfirmPassword = "JohnDoe33",
            PersonRole = Role.Instructor,
            Email = "JohnDoeInstructor@clubinternet.com",
            HireDate = DateTime.Now
        };

        private readonly LoginVM LoginStudent = new LoginVM()
        {
            UserName = "student",
            Password = "student"
        };

        #endregion


        #region TestsConfiguration
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
        
        #endregion

        #region TestsRegister

        [Test]
        public void Register_RegisterStudent_ViewLogin()
        {
            RedirectToRouteResult result = controllerToTest.Register(RegisterStudent) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Authentication"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Login"));
        }

        [Test]
        public void Register_RegisterInstructor_ViewLogin()
        {
            RedirectToRouteResult result = controllerToTest.Register(RegisterInstructor) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Authentication"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Login"));
        }

        #endregion

        #region TestRegisterPropertyMissing

        //[Test]
        //public void Register_Instructor_PropertyMissing_FirstName()
        //{
        //    EntityGenerator generator = new EntityGenerator(dbContext);
        //    RegisterVM vm = generator.RegisterVM(ViewModels.Role.Instructor);
        //    vm.FirstMidName = null;
        //    ViewResult result = new ViewResult();

        //    Assert.That(() => result = controllerToTest.Register(vm) as ViewResult,
        //            Throws.TypeOf<System.Data.Entity.Validation.DbEntityValidationException>()
        //            .With.Message.EqualTo(VALIDATION_ERROR_MSG)
        //        );
        //}

        //[Test]
        //public void Register_Instructor_PropertyMissing_LastName()
        //{
        //    EntityGenerator generator = new EntityGenerator(dbContext);
        //    RegisterVM vm = generator.RegisterVM(ViewModels.Role.Instructor);
        //    vm.LastName = null;
        //    ViewResult result = new ViewResult();

        //    Assert.That(() => result = controllerToTest.Register(vm) as ViewResult,
        //            Throws.TypeOf<System.Data.Entity.Validation.DbEntityValidationException>()
        //            .With.Message.EqualTo(VALIDATION_ERROR_MSG)
        //        );
        //}

        //[Test]
        //public void Register_Instructor_PropertyMissing_Password()
        //{
        //    EntityGenerator generator = new EntityGenerator(dbContext);
        //    RegisterVM vm = generator.RegisterVM(ViewModels.Role.Instructor);
        //    vm.Password = null;
        //    ViewResult result = new ViewResult();

        //    Assert.That(() => result = controllerToTest.Register(vm) as ViewResult,
        //            Throws.TypeOf<System.Data.Entity.Validation.DbEntityValidationException>()
        //            .With.Message.EqualTo(VALIDATION_ERROR_MSG)
        //        );
        //}

        //[Test]
        //public void Register_Instructor_PropertyMissing_UserName()
        //{
        //    EntityGenerator generator = new EntityGenerator(dbContext);
        //    RegisterVM vm = generator.RegisterVM(ViewModels.Role.Instructor);
        //    vm.UserName = null;
        //    ViewResult result = new ViewResult();

        //    Assert.That(() => result = controllerToTest.Register(vm) as ViewResult,
        //            Throws.TypeOf<System.Data.Entity.Validation.DbEntityValidationException>()
        //            //.With.Message.EqualTo(VALIDATION_ERROR_MSG)
        //        );
        //}

        #endregion


        #region TestsLogin
        [Test]
        public void Login_LoginStudent_ViewHomeIndex()
        {
            EntityGenerator generator = new EntityGenerator(dbContext);
            Student s = new Student();

            s = generator.CreateStudent("Doe", "John", LoginStudent.UserName, LoginStudent.Password, "johndoe@aol.com");

            RedirectToRouteResult result = controllerToTest.Login(LoginStudent) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));

            Assert.That(httpContext.SessionState, Is.Not.Null);
        }

        [Test]
        public void Login_LoginStudent_WrongPassword()
        {
            EntityGenerator generator = new EntityGenerator(dbContext);
            Student student = new Student();
            //s = generator.CreateStudent(RegisterVM_Student_JohnDoe);
            student = generator.CreateStudent(RegisterStudent);

            string wrongPassword = "wrong password";
            LoginVM testStudent = new LoginVM { UserName = student.UserName, Password = wrongPassword };

            ViewResult result = controllerToTest.Login(testStudent) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewData["Error"], Is.EqualTo("Invalid login or password."));
            //Any way to test that ViewResult result is the "Login" view???
        }

        [Test]
        public void Login_LoginInstructor_WrongPassword()
        {
            EntityGenerator generator = new EntityGenerator(dbContext);
            Instructor instructor = new Instructor();
            instructor = generator.CreateInstructor(RegisterInstructor);

            string wrongPassword = "wrong password";
            LoginVM testInstructor = new LoginVM { UserName = instructor.UserName, Password = wrongPassword };

            ViewResult result = controllerToTest.Login(testInstructor) as ViewResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewData["Error"], Is.EqualTo("Invalid login or password."));
            //Any way to test that ViewResult result is the "Login" view???
        }

        [Test]
        public void Login_LoginInstructor_ViewHomeIndex()
        {
            EntityGenerator generator = new EntityGenerator(dbContext);
            generator.CreateInstructor("Doe", "John", LoginInstructor.UserName, LoginInstructor.Password, "johndoe@aol.com");

            RedirectToRouteResult result = controllerToTest.Login(LoginInstructor) as RedirectToRouteResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Login_LoginIsFoundInDB()
        {
            string LoginToTest = RegisterInstructor.UserName;
            EntityGenerator generator = new EntityGenerator(dbContext);
            Instructor instructor = generator.CreateInstructor(RegisterInstructor);
            Assert.That(dbContext.Instructors.Find(instructor.ID).UserName, Is.EqualTo(LoginToTest));
        }

        [Test]
        public void Login_PasswordIsSaltedAndHashedInDB()
        {
            string PasswordToTest = RegisterInstructor.Password;
            EntityGenerator generator = new EntityGenerator(dbContext);
            Instructor instructor = generator.CreateInstructor(RegisterInstructor);
            Assert.That(instructor.Password, Is.Not.EqualTo(PasswordToTest));
            Assert.That(instructor.Password, Is.EqualTo(Authentication.SaltAndHash(PasswordToTest)));
        }

        #endregion

    }
}
