using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Project.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Book> books = new List<Book>();
            using (TheLazyNoodleEntities1 con = new TheLazyNoodleEntities1())
            {
                books = con.Books.ToList();
            }
            return View(books);
        }
    }
}