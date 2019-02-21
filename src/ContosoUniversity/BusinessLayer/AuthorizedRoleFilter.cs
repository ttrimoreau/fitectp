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

            
            if (HttpContext.Current.Session["UserId"] == null)
            {
                return false;
            }
            else if (Role.ToString() == "Student" && !(HttpContext.Current.Session["UserRole"].ToString()=="Student"))
            {
                return false;
            }
            else if (Role.ToString() == "Instructor" && !(HttpContext.Current.Session["UserRole"].ToString()=="Instructor"))
            {
                return false;
            }
            return true;
        }
    }
}
