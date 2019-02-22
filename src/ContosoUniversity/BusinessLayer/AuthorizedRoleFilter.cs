using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ContosoUniversity.BusinessLayer
{
    public class AuthorizedRoleFilterAttribute : AuthorizeAttribute
    {

        public string Role { get; set; }
        protected override bool AuthorizeCore(HttpContextBase filterContext)
        {

            
            if (HttpContext.Current.Session[SessionMessage.UserID] == null)
            {
                return false;
            }
            else if (Role.ToString() == SessionMessage.StudentRole && !(HttpContext.Current.Session[SessionMessage.UserRole].ToString()==SessionMessage.StudentRole))
            {
                return false;
            }
            else if (Role.ToString() == SessionMessage.InstructorRole && !(HttpContext.Current.Session[SessionMessage.UserRole].ToString()==SessionMessage.InstructorRole))
            {
                return false;
            }
            return true;
        }
    }
}
