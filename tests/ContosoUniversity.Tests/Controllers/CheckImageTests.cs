using ContosoUniversity.BL;
using ContosoUniversity.Controllers;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.Tests.Tools;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity.Tests.Controllers
{
    public class CheckImageTests : IntegrationTestsBase
    {
        private MockHttpContextWrapper httpContext;
        private StudentController controllerToTest;
        private SchoolContext dbContext;

        [SetUp]
        public void Initialize()
        {
            httpContext = new MockHttpContextWrapper();
            controllerToTest = new StudentController();
            controllerToTest.ControllerContext = new ControllerContext(httpContext.Context.Object, new RouteData(), controllerToTest);
            dbContext = new DAL.SchoolContext(this.ConnectionString);
            controllerToTest.DbContext = dbContext;
        }

        string expectedlogin = "login";
        string expectedpassword = "password";

        [SetUp] //to initialize a user to authentification test
        public void CreationOfUserToTest()
        {
            EntityGenerator generator = new EntityGenerator(dbContext);
            Student student = generator.CreateStudentUser(expectedlogin, expectedpassword);
        }



        [Test]
        public void UploadImage_Student_Success()
        {
            string expectedLastName = "Wood";
            string previousLastName = "Dubois";
            string previousFirstName = "George";



            EntityGenerator generator = new EntityGenerator(dbContext);
            Student student = generator.CreateStudentForUploadImage(previousLastName, previousFirstName);
            student.LastName = expectedLastName;

            FormDataHelper.PopulateFormData(controllerToTest, student);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("whatever"));

            MyTestPostedFileBase test = new MyTestPostedFileBase(stream, "whatever", "testImage.png");

            var result = controllerToTest.EditPost(student.ID, test) as ViewResult;

            Student savedStudent = dbContext.Students.Find(student.ID);
            FileImage savedImage = dbContext.FileImages.Find(student.ID);


            Assert.That(result, Is.Not.Null);
            Assert.That(savedStudent, Is.Not.Null);
            Assert.That(savedImage, Is.Not.Null);
        }
        [Test]
        public void UploadImage_Instructor_Success()
        {
            //var stream = new MemoryStream(Encoding.UTF8.GetBytes("whatever"));

            //MyTestPostedFileBase test = new MyTestPostedFileBase(stream, "whatever", "testImage.png");
            //var result = controllerToTest.EditPost(25, test) as RedirectToRouteResult;
            //Assert.AreEqual("Index", result.RouteValues["action"]);
            //Assert.AreEqual("Student", result.RouteValues["controller"]);
            Assert.True(false);
        }

        [Test]
        public void UploadImage_Success()
        {
            
            Assert.True(false);
        }


        [TestCase(".tiff")]
        [TestCase(".bmp")]
        [TestCase(".gif")]
        public void TestImage_Extension_Other_Result_True(string extension)
        {
            CheckImage checkImage = new CheckImage();
            string FileName = $"testImage{extension}";
            bool result = checkImage.checkExtension(FileName);
            Assert.False(result);
        }

        [TestCase(".png")]
        [TestCase(".jpeg")]
        [TestCase(".jpg")]
        public void TestImage_Extension_PNG_JPEG_Result_True(string extension)
        {
            CheckImage checkImage = new CheckImage();
            string FileName = $"testImage{extension}";
            bool result = checkImage.checkExtension(FileName);
            Assert.True(result);
        }

        [Test]
        public void TestImage_Size_MoreThan_100kb_Result_True()
        {
            CheckImage checkImage = new CheckImage();
            long FileSize = 1000000;
            bool result = checkImage.checkSize(FileSize);
            Assert.False(result);
        }

        [Test]
        public void TestImage_Size_LessThan_100kb_Result_True()
        {
            CheckImage checkImage = new CheckImage();
            long FileSize = 1000;
            bool result = checkImage.checkSize(FileSize);
            Assert.True(result);
        }

        [TearDown] //to clean db after each test
        public void CleanUpDb()
        {
            SettingUpTests();
        }
    }
}
