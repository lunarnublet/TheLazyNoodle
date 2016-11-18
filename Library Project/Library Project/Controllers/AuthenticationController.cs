using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Project.Controllers
{
    using Services;
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        [HttpGet]
        public ActionResult Login()
        {
            return View(new UserProfile());
        }

        [HttpPost]
        public ActionResult Login(UserProfile user)
        {
            //if (ModelState.IsValid)
            //{
            var auth = new AuthenticationService(user.username, user.password);

            if (auth.IsValidPassword())
            {
                Session["userId"] = auth.GetUserId();
                return RedirectToAction("Index", "Home");
            }

            //}

            return View(user); //TODO(dallin): validation
        }
    }
}