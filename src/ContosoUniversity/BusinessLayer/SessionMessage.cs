using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.BusinessLayer
{
    public static class SessionMessage
    {

        #region StringSession
        public static string UserID = "UserId";
        public static string UserRole = "UserRole";
        public static string User = "User";
        #endregion

        #region Role
        public static string StudentRole = "Student";
        public static string InstructorRole = "Instructor";
        #endregion
    }
}