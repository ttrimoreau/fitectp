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
    [AuthorizedRoleFilter(Role = "Instructor", Roles = "Student")]
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
        [AllowAnonymous]
        // GET: Authentication
        public ActionResult Login()
        {
            LoginVM viewModel = new LoginVM() { Authentified = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                viewModel.Person = ObtainUser(HttpContext.User.Identity.Name);
            }

            return View(viewModel);
            
        }


        [AllowAnonymous]
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName,Password")]LoginVM vmlogin)
        {
            if (ModelState.IsValid)
            {
                string HashedAndSaltedPassword = Authentication.SaltAndHash(vmlogin.Password);
                Person user = db.People.SingleOrDefault(x => x.UserName == vmlogin.UserName && x.Password == HashedAndSaltedPassword);
                if (user!=null)
                {
                    
                    
                    
                   int id = db.People.Single(x => x.UserName == vmlogin.UserName).ID;

                    Session["UserId"] = id;
                    if (id!=0)
                    {
                        Session["UserRole"] = "Instructor";
                    }

                    if (db.Students.Single(i => i.ID == id) != null)
                    {
                        Session["UserRole"] = "Student";
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
        public ActionResult Register([Bind(Include = "LastName,FirstMidName,UserName,Password,Email,HireDate,ConfirmPassword")]RegisterVM registerVM)
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
        // GET: Authentication
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        } 
        #endregion


    }
}