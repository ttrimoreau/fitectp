using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.BusinessLayer;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers
{

    public class AuthenticationController : Controller
    {
        private SchoolContext db = new SchoolContext();


        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        //POST
        [HttpPost]
        public ActionResult Login(LoginVM vmlogin)
        {
            if (ModelState.IsValid)
            {
                string HashedAndSaltedPassword = Authentication.SaltAndHash(vmlogin.Password);
                if(db.People.Any(x => x.UserName == vmlogin.UserName && x.Password == HashedAndSaltedPassword))
                {
                    Session["UserId"] = db.People.Single(x => x.UserName == vmlogin.UserName).ID;
                }
            }


            return View();
        }

        // GET: Authentication
        public ActionResult Register()
        {
            return View();
        }

        // POST: Authentication
        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            return View();
        }




    }
}