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
            return View(model: new Book());
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
                    context.SaveChangesAsync();
                }

                result = RedirectToAction("Index", "Home");
            }

            return result;
        }

        [HttpPost]
        public ActionResult DeleteBook(int id)
        {
            using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
            {
                Book book = new Book { Id = id };
                context.Entry(book).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditBook(int id)
        {
            Book bookToEdit = null;
            using (TheLazyNoodleEntities1 context = new TheLazyNoodleEntities1())
            {
                bookToEdit = context.Books.Include("Authors").Where(b => b.Id == id).Single();
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