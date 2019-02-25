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

            
            if (HttpContext.Current.Session[CONSTANTS.UserID] == null)
            {
                return false;
            }
            else if (Role.ToString() == CONSTANTS.StudentRole && !(HttpContext.Current.Session[CONSTANTS.UserRole].ToString()== CONSTANTS.StudentRole))
            {
                return false;
            }
            else if (Role.ToString() == CONSTANTS.InstructorRole && !(HttpContext.Current.Session[CONSTANTS.UserRole].ToString()== CONSTANTS.InstructorRole))
            {
                return false;
            }
            return true;
        }
    }
}
