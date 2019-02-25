using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BusinessLayer
{
    public static class CONSTANTS
    {
        public const string SHORT_DATE = "yyyy-MM-dd";

        #region CheckImage
        public const string ErrorSize = "The size of the image is limited to 100kb";
        public const string ErrorExtension = "Image extention authorized is .png, .jpeg or .jpg";
        #endregion

        #region Authentication
        public const string LoginMessage = "Invalid login or password.";
        public const string ErrorRegisterUserNameUnavailable = "This UserName is already taken";
        #endregion

        #region StringSession
        public const string UserID = "UserId";
        public const string UserRole = "UserRole";
        #endregion

        #region Role
        public const string StudentRole = "Student";
        public const string InstructorRole = "Instructor";
        #endregion











    }
}