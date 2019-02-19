using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.BusinessLayer;

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
        public ActionResult Login(VMLogin vmlogin)
        {
            if (vmlogin.isvalid)
            {
                string HashedAndSaltedPassword = Authentication.SaltAndHash(vmlogin.Password);
                if(db.People.Any(x => x.UserName == vmlogin.UserName && x.Password == HashedAndSaltedPassword))
                {
                    Session["UserId"] = db.People.Find(x => x.UserName == vmlogin.UserName).ID;
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
        public ActionResult Register()
        {
            return View();
        }




    }
}