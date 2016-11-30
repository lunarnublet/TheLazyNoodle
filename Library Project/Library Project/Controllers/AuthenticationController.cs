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
            var auth = new LoginService(user.username, user.password);

            if (auth.IsValidPassword())
            {
                System.Web.HttpContext.Current.Session["userId"] = auth.GetUserId();
                Session["userId"] = auth.GetUserId();
                return RedirectToAction("Index", "Home");
            }

            //}

            return View(user); //TODO(dallin): validation
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View(new UserProfile());
        }

        [HttpPost]
        public ActionResult SignUp(UserProfile user)
        {
            var auth = new SignUpService(user.username, user.password);

            try
            {
                auth.Persist();
                return RedirectToAction("Index", "Home");
            }
            catch (InvalidOperationException /*e*/)
            {
                // TODO(dallin): display validation errors
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            Session["userId"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}