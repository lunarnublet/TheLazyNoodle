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
                int userId = 0;
                try
                {
                    userId = (int)Session["userId"];

                }
                catch
                {
                    userId = 0;
                }
                p = con.UserProfiles.Where(s => s.Id == userId).ToList().SingleOrDefault();
                if (p != null)
                {
                    p.Roles = p.Roles.ToList();
                    p.Books = p.Books.ToList();
                }
                else
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
            return View(p);
        }
    }
}