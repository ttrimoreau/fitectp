using ContosoUniversity.BL;
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
    public class CheckImageTests : IntegrationTestsBase
    {
        [Test]
        public void UploadImage_Success()
        {

            Assert.True(false);
        }

        [Test]
        public void DuplicateImage_Fail()
        {

            Assert.True(false);
        }
        [Test]
        public void TwoImage_For_One_Id_Fail()
        {

            Assert.True(false);
        }


        [TestCase("tiff")]
        [TestCase("bmp")]
        [TestCase("gif")]
        public void TestImage_Extension_Other_Result_True(string extension)
        {
            CheckImage checkImage = new CheckImage();
            string FileName = $"testImage.{extension}";
            bool result = checkImage.checkExtension(FileName);
            Assert.False(result);
        }

        [TestCase("png")]
        [TestCase("jpeg")]
        public void TestImage_Extension_PNG_JPEG_Result_True(string extension)
        {
            CheckImage checkImage = new CheckImage();
            string FileName = $"testImage.{extension}";
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
    }
}
