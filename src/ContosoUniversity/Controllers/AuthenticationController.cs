﻿using System;
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
        public SchoolContext DbContext
        {
            get { return db; }
            set { db = value; }
        }

        [HttpPost]
        public JsonResult IsAlreadySigned(string username)
        {
           System.Threading.Thread.Sleep(1000);
            UsernameAvailable  user = new UsernameAvailable();
            bool available = user.UsernameIsAvailable(username);
            return Json(available);
            //bool prevUser = !db.People.ToList().Exists(c => c.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
            //return Json(prevUser);

            //if (prevUser. == username)
            //{
            //    return Json(false);
            //}
            //else
            //{
            //    return Json(true);
            //}
        }

        // GET: Authentication
        public ActionResult Index()
        {
            return View("Login", "Authentication");
        }

        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }

        // GET: Authentication
        public ActionResult Logout()
        {
            Session["UserId"] = null;
            return RedirectToAction("Index", "Home");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName,Password")]LoginVM vmlogin)
        {
            if (ModelState.IsValid)
            {
                string HashedAndSaltedPassword = Authentication.SaltAndHash(vmlogin.Password);
                if (db.People.Any(x => x.UserName == vmlogin.UserName && x.Password == HashedAndSaltedPassword))
                {
                    Session["UserId"] = db.People.Single(x => x.UserName == vmlogin.UserName).ID;
                }
                else
                {
                    ViewData["Error"] = "Invalid login or password.";
                    return View();
                }
            }


            return RedirectToAction("Index", "Home");
        }

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




    }
}