using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Project.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserProfile(UserProfile profile)
        {
            return View(model:profile);
        }
    }
}