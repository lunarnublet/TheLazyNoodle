using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Project.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login(string userName, string password)
        {
            using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
            {
            }
            return View();
        }
    }