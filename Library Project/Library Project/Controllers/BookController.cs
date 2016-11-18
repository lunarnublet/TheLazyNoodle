using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Project.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        [HttpGet]
        public ActionResult AddBook()
        {
            int userId = 0;
            try
            {
                userId = (int)Session["userId"];
            }
            catch (NullReferenceException)
            {
                userId = 0;
            }

            using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
            {
                UserProfile profile = context.UserProfiles.Where(s => s.Id == userId).SingleOrDefault();
                if (profile != null)
                {
                    profile.Books.ToList();
                    profile.Roles.ToList();
                    if (profile.Roles.Where(r => r.roleName.Contains("Admin")).SingleOrDefault() != null)
                    {
                        return View(model: new Book());
                    }
                    else
                    {
                        return RedirectToAction("Login", "Authentication");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }
        }

        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            ActionResult result = View(model: book);

            if (ModelState.IsValid) // TODO(Dallin): Validation
            {
                using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
                {
                    
                            context.Books.Add(book);
                            context.SaveChanges();
                }
                result = RedirectToAction("Index", "Home");
                
            }

            return result;
        }

        
        public ActionResult DeleteBook(int id)
        {
            using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
            {
                int userId = 0;
                try
                {
                    userId = (int)Session["userId"];
                }
                catch (NullReferenceException ex)
                {
                    userId = 0;
                }
                UserProfile profile = context.UserProfiles.Where(s => s.Id == userId).SingleOrDefault();
                if (profile != null)
                {
                    profile.Books.ToList();
                    profile.Roles.ToList();
                    if (profile.Roles.Where(r => r.roleName.Contains("Admin")).SingleOrDefault() != null)
                    {
                        Book book = new Book { Id = id };
                        context.Entry(book).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("Login", "Authentication");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Authentication");
                }
                
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditBook(int id)
        {
            Book bookToEdit = null;
            using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
            {
                int userId = 0;
                try
                {
                    userId = (int)Session["userId"];
                }
                catch (NullReferenceException ex)
                {
                    userId = 0;
                }
                UserProfile profile = context.UserProfiles.Where(s => s.Id == userId).SingleOrDefault();
                if (profile != null)
                {
                    profile.Books.ToList();
                    profile.Roles.ToList();
                    if (profile.Roles.Where(r => r.roleName.Contains("Admin")).SingleOrDefault() != null)
                    {
                        bookToEdit = context.Books.Include("Authors").Where(b => b.Id == id).Single();
                    }
                    else
                    {
                        return RedirectToAction("Login", "Authentication");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Authentication");
                }
            }

            return View(model: bookToEdit);
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            ActionResult result = View(model: book);

            if (ModelState.IsValid) // TODO(Dallin): Validation
            {
                using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
                {
                    Book bookToEdit = context.Books.Find(book.Id);

                    if (bookToEdit != null)
                    {
                        bookToEdit.Title = book.Title;
                        bookToEdit.PublishYear = book.PublishYear;
                        //bookToEdit.Authors = book.Authors; //// this causes duplication
                        context.SaveChanges();
                    }
                }

                result = RedirectToAction("Index", "Home");
            }

            return result;
        }

        private void UpdateBookAuthors(Book bookToUpdate, Book updatedBook)
        {
            foreach(Author authorToUpdate in bookToUpdate.Authors)
            {
                Author updatedAuthor = updatedBook.Authors.SingleOrDefault(a => a.Id == authorToUpdate.Id);
            }
        }
    }
}