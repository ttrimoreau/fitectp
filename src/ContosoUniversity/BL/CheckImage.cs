using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BL
{
    public class CheckImage
    {
        public CheckImage()
        {

        }

        bool testExt = false;
        public bool checkExtension(string fileName)
        {
            string fileNameTest = fileName.ToLower();
            if (fileNameTest == ".png" || fileNameTest == ".jpeg" || fileNameTest==".jpg")
            {
                testExt = true;

            }
            return testExt;
        }


        bool testSize = false;
        public bool checkSize(long fileSize)
        {

            if (fileSize < 100000)
            {
                testSize = true;
            }
            return testSize;
        }
    }
}