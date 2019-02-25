using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.BusinessLayer;
using ContosoUniversity.ViewModels;
using ContosoUniversity.Models;
using System.Web.Security;
using ContosoUniversity.BL;

namespace ContosoUniversity.Controllers
{
    
    public class AuthenticationController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        #region ObtainUser
        public Person ObtainUser(int id)
        {
            return db.People.FirstOrDefault(u => u.ID == id);
        }

        public Person ObtainUser(string idString)
        {
            int id;
            if (int.TryParse(idString, out id))
                return ObtainUser(id);
            return null;
        }
        #endregion

        #region Login
        
        // GET: Authentication
        public ActionResult Login()
        {
            LoginVM viewModel = new LoginVM();
            return View(viewModel);
        }

        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM vmlogin)
        {
            if (ModelState.IsValid)
            {
                string HashedAndSaltedPassword = Authentication.SaltAndHash(vmlogin.Password);
                Person user = db.People.SingleOrDefault(x => x.UserName == vmlogin.UserName && x.Password == HashedAndSaltedPassword);
                if (user!=null)
                {
                    Session[CONSTANTS.UserID] = user.ID;
                    if(user is Student)
                    {
                        Session[CONSTANTS.UserRole] = CONSTANTS.StudentRole;
                    }
                    else
                    {
                        Session[CONSTANTS.UserRole] = CONSTANTS.InstructorRole;
                    }
                    FormsAuthentication.SetAuthCookie(user.ID.ToString(), false);
                }
                else
                {
                    ViewData["Error"] = CONSTANTS.LoginMessage;
                    return View();
                }

            }
            else
            {
                ViewData["Error"] = CONSTANTS.LoginMessage;
                return View();
            }

            return RedirectToAction("Index", "Home");
        } 
        #endregion

        #region Register
        // GET: Authentication
        public ActionResult Register()
        {
            return View();
        }

        // POST: Authentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                //if UserName is already taken
                if (db.People.Any(x => x.UserName == registerVM.UserName))
                {
                    ViewData["Error"] = CONSTANTS.ErrorRegisterUserNameUnavailable;
                    return View();
                }
                else
                {
                    Authentication.CreatePerson(registerVM);
                    return RedirectToAction("Login", "Authentication");
                }
            }

            return View();
        }
        #endregion

        #region LogOut
        [AuthorizedRoleFilter(Role = "Student", Roles = "Instructor")]
        // GET: Authentication
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        #endregion


    }
}