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

        // GET: Authentication
        public ActionResult Index()
        {
            return View("Login","Authentication");
        }

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
                    Session[SessionMessage.UserID] = user.ID;
                    if ((db.Students.FirstOrDefault(p => p.ID == user.ID)) != null)
                    {

                        Session[SessionMessage.UserRole] = SessionMessage.StudentRole;
                    }
                    else
                    {
                        Session[SessionMessage.UserRole] = SessionMessage.InstructorRole;
                    }
                    FormsAuthentication.SetAuthCookie(user.ID.ToString(), false);
                }
                
                else
                {
                    ViewData["Error"] = "Invalid login or password.";
                    return View();
                }
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
        public ActionResult Register([Bind(Include = "LastName,FirstMidName,UserName,Password,Email,HireDate,ConfirmPassword,PersonRole")]RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                //if UserName is already taken
                if (db.People.Any(x => x.UserName == registerVM.UserName))
                {
                    ViewData["Error"] = "This UserName is already taken";
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