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

        [HttpPost]
        public ActionResult Index(Book book)
        {
            List<Book> books = new List<Book>();
            using (TheLazyNoodleEntities1 con = new TheLazyNoodleEntities1())
            {
                books = con.Books.ToList();
                if (book.Title != null && book.Title != "")
                {
                    List<Book> temp = con.Books.Where(b => b.Title.Contains(book.Title)).ToList();
                    books = temp.Intersect(books).ToList();
                }
                if (book.PublishYear != null)
                {
                    List<Book> temp = con.Books.Where(b => b.PublishYear == book.PublishYear).ToList();
                    books = temp.Intersect(books).ToList();
                }
            }
            return View("Index", books);
        }

        public ActionResult Search()
        {
            return View(new Book());
        }

        [HttpPost]
        public ActionResult Search(Book book)
        {
            return Index(book);
        }
    }
}