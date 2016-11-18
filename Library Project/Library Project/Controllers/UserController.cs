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
            UserProfile p = new UserProfile();
            using (TheLazyNoodleEntities1 con = new TheLazyNoodleEntities1())
            {
                Session["userId"] = 1;
                int userId = (int)Session["userId"];
                p = con.UserProfiles.Where(s => s.Id == userId).ToList().SingleOrDefault();
                p.Roles = p.Roles.ToList();
                p.Books = p.Books.ToList();
            }
            return View(p);
        }
    }
}