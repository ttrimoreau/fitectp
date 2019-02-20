using ContosoUniversity.Enum;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BusinessLayer
{
    public class UploadImage
    {
        #region Constructeur
        public UploadImage()
        {

        }
        #endregion

        #region UploadImage
        public FileImage Upload(HttpPostedFileBase upload)
        {
            var avatar = new FileImage
            {
                
                FileType = FileType.Avatar,
                ContentType = upload.ContentType
            };
            using (var reader = new System.IO.BinaryReader(upload.InputStream))
            {
                avatar.Content = reader.ReadBytes(upload.ContentLength);
            }

            return avatar;
        }


        #endregion
    }
}