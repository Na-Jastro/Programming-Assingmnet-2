using Programming_Assignment_2.Controllers;
using Programming_Assignment_2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Programming_Assignment_2.Controllers
{


    public class AccountController : Controller
    {
        //Return Home page.
        public ActionResult Index()
        {
            return View();
        }
        //Return Register view
        public ActionResult Register()
        {
            return View();
        }
        //==============================Register User==========================================//
        [HttpPost]
        public ActionResult SaveRegisterDetails(Register registerDetails)
        {
            MyDatabaseEntities entities = new MyDatabaseEntities();
            if (entities.User.Any(a => a.Email.Equals(registerDetails.Email)))
            {
                ViewBag.Message = "Email Already Taken";
                return View("Register");
            }

            if (ModelState.IsValid)
            {
                //===============Save all details in RegitserUser object=============================//
                using (var databaseContext = new MyDatabaseEntities())
                {
                    User registerUser = new User();
                    registerUser.FirstName = registerDetails.FirstName;
                    registerUser.LastName = registerDetails.LastName;
                    registerUser.Email = registerDetails.Email;
                    registerUser.Password = registerDetails.Password;
                    databaseContext.User.Add(registerUser);
                    databaseContext.SaveChanges();
                }
                ViewBag.Message = "User Details Saved";
                return View("Register");
            }
            else
            {
                return View("Register", registerDetails);
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        //Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isValidUser = IsValidUser(model);
                if (isValidUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }

        //===========================function to check if User is valid or not============================//
        public User IsValidUser(LoginViewModel model)
        {
            using (var dataContext = new MyDatabaseEntities())
            {
                User user = dataContext.User.Where(query => query.Email.Equals(model.Email) && query.Password.Equals(model.Password)).SingleOrDefault();
                if (user == null)
                    return null;
                else
                    return user;
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index");
        }
    }
}