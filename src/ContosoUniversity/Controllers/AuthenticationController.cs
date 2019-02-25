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

        // GET: Authentication
        public ActionResult Index()
        {
            return View(nameof(AuthenticationController.Login),"Authentication");
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
                UserSession userSession = new UserSession();
                Person userConnected = userSession.GetPersonByUserName(vmlogin.UserName, db);

                string HashedAndSaltedPassword = Authentication.SaltAndHash(vmlogin.Password);
                //Person user = db.People.SingleOrDefault(x => x.UserName == vmlogin.UserName && x.Password == HashedAndSaltedPassword);
                if (userConnected!=null && userConnected.Password== HashedAndSaltedPassword)
                {
                    Session[SessionMessage.User] = userConnected;
                    Session[SessionMessage.UserID] = userConnected.ID;
                    if ((db.Students.FirstOrDefault(p => p.ID == userConnected.ID)) != null)
                    {

                        Session[SessionMessage.UserRole] = SessionMessage.StudentRole;
                    }
                    else
                    {
                        Session[SessionMessage.UserRole] = SessionMessage.InstructorRole;
                    }
                    FormsAuthentication.SetAuthCookie(userConnected.ID.ToString(), false);
                }
                
                else
                {
                    ViewData["Error"] = ErrorMessages.LoginMessage;
                    return View();
                }

            }
            else
            {
                ViewData["Error"] = ErrorMessages.LoginMessage;
                return View();
            }


            return RedirectToAction(nameof(HomeController.Index), "Home");
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
                    return RedirectToAction(nameof(AuthenticationController.Login), nameof(AuthenticationController));
                }
            }

            return View();
        }
        #endregion

        #region LogOut
        //[AuthorizedRoleFilter(Role = "Student", Roles = "Instructor")]

        // GET: Authentication
        public ActionResult LogOut()
        {
            if (Session[SessionMessage.User]==null)
            {
                return RedirectToAction(nameof(HomeController.About), "Home");
            }
            Session.RemoveAll();
            Session.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
        #endregion


    }
}