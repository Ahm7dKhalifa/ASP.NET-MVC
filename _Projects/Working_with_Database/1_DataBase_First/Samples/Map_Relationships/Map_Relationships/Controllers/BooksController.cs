using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Map_Relationships.Models;
using System.Data.SqlClient;

namespace Map_Relationships.Controllers
{
    public class BooksController : Controller
    {
        private RelationshipsEntities db = new RelationshipsEntities();

        // GET: Books
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_Book_Id,Name,ISBN")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Book_Id,Name,ISBN")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*#######################################################
         * entity framework with native sql 
         * ######################################################*/
        /*-----------------------------------
         *  list of all row (index method)
         *  ---------------------------------*/
        //return all rows in the Book Table
        public ActionResult MyIndex()
        {
            List<Book> MyBooks = db.Books.SqlQuery("SELECT * FROM Book").ToList<Book>();
            return View(MyBooks);
        }
        /*-----------------------------------
         *  Details Method
         *  ---------------------------------*/
        //return only one row in the Book Table
        public ActionResult MyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var MyID = new SqlParameter("ID", id);
            Book MyBook = db.Books.SqlQuery(@"Select * from Book WHERE Pk_Book_Id = @ID", MyID).Single<Book>();
            if (MyBook == null)
            {
                return HttpNotFound();
            }
            return View(MyBook);
        }
        /*-----------------------------------
        *  create Method
        *  ---------------------------------*/
        //INSERT OBJECT(ROW IN BOOK TABLE)
        public ActionResult MyCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyCreate([Bind(Include = "Pk_Book_Id,Name,ISBN")] Book book)
        {
            if (ModelState.IsValid)
            {
                int MyNewBook = db.Database.ExecuteSqlCommand(@"INSERT INTO Book VALUES (@id,@name,@isbn)", new SqlParameter("id", book.Pk_Book_Id), new SqlParameter("name", book.Name), new SqlParameter("isbn", book.ISBN));
                db.SaveChanges();
                return RedirectToAction("MyIndex");
            }

            return View(book);
        }
        /*-----------------------------------
        *  Edit Method
        *  ---------------------------------*/
        //Edit row in database 
        //GET : BOOKS/MyEdit/2
        public ActionResult MyEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var MyID = new SqlParameter("ID", id);
            Book MyBook = db.Books.SqlQuery(@"Select * from Book WHERE Pk_Book_Id = @ID", MyID).Single<Book>();
            if (MyBook == null)
            {
                return HttpNotFound();
            }
            return View(MyBook);  
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEdit([Bind(Include = "Pk_Book_Id,Name,ISBN")] Book book)
        {
            if (ModelState.IsValid)
            {
                int MyNewBook = db.Database.ExecuteSqlCommand(@"UPDATE Book SET Name = @name , ISBN = @isbn WHERE Pk_Book_Id = @id ", new SqlParameter("name", book.Name), new SqlParameter("isbn", book.ISBN),new SqlParameter("id",book.Pk_Book_Id));
                db.SaveChanges();
                return RedirectToAction("MyIndex");
            }

            return View(book);
        }
        /*-----------------------------------
        *  Delete Method
        *  ---------------------------------*/
        //Delete row in database 
        //GET : BOOKS/MyDelete/2
        public ActionResult MyDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var MyID = new SqlParameter("ID", id);
            Book MyBook = db.Books.SqlQuery(@"Select * from Book WHERE Pk_Book_Id = @ID", MyID).Single<Book>();
            if (MyBook == null)
            {
                return HttpNotFound();
            }
            return View(MyBook);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyDelete(int id)
        {
                int MyBook = db.Database.ExecuteSqlCommand(@"DELETE FROM Book WHERE Pk_Book_Id = @id ", new SqlParameter("id", id));
                db.SaveChanges();
                return RedirectToAction("MyIndex");
        }
        /*-----------------------------------
        *  join two table 
        *  ---------------------------------*/
        public ActionResult Book_Author()
        {
            List<MyBookAuthor> MyBooks = db.Database.SqlQuery<MyBookAuthor>("SELECT Pk_Book_Id,Name,ISBN,Pk_Author_Id,FullName,MobileNo,Fk_Book_Id FROM Book, Author where Book.Pk_Book_Id = Author.Fk_Book_Id; ").ToList<MyBookAuthor>();
            return View(MyBooks);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
