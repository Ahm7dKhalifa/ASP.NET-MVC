

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
    public class AuthorsController : Controller
    {
        private RelationshipsEntities db = new RelationshipsEntities();

        // GET: Authors
        public ActionResult Index()
        {
            var authors = db.Authors.Include(a => a.Book);
            return View(authors.ToList());
        }

        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            ViewBag.Fk_Book_Id = new SelectList(db.Books, "Pk_Book_Id", "Name");
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_Author_Id,FullName,MobileNo,Fk_Book_Id")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fk_Book_Id = new SelectList(db.Books, "Pk_Book_Id", "Name", author.Fk_Book_Id);
            return View(author);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_Book_Id = new SelectList(db.Books, "Pk_Book_Id", "Name", author.Fk_Book_Id);
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Author_Id,FullName,MobileNo,Fk_Book_Id")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fk_Book_Id = new SelectList(db.Books, "Pk_Book_Id", "Name", author.Fk_Book_Id);
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*#######################################################
         * entity framework with native sql 
        * ######################################################*/
        /*-----------------------------------
         *  list of all row (index method)
         *  ---------------------------------*/
        //return all rows in the Author Table
        public ActionResult MyIndex()
        {
            List<Author> MyAuthor = db.Authors.SqlQuery("SELECT * FROM Author").ToList<Author>();
            return View(MyAuthor);
        }
        /*-----------------------------------
         *  Details Method
         *  ---------------------------------*/
        //return only one row in the Author Table
        public ActionResult MyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var MyID = new SqlParameter("ID", id);
            Author MyAuthor = db.Authors.SqlQuery(@"Select * from Author WHERE Pk_Author_Id = @ID", MyID).Single<Author>();
            if (MyAuthor == null)
            {
                return HttpNotFound();
            }
            return View(MyAuthor);
        }
        /*-----------------------------------
        *  create Method
        *  ---------------------------------*/
        //INSERT OBJECT(ROW IN AuthorTABLE)
        public ActionResult MyCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyCreate([Bind(Include = "Pk_Author_Id,FullName,MobileNo,Fk_Book_Id")] Author Author)
        {
            if (ModelState.IsValid)
            {
                int MyNewAuthor = db.Database.ExecuteSqlCommand(@"INSERT INTO Author VALUES (@id,@name,@mob,@fk)", new SqlParameter("id", Author.Pk_Author_Id), new SqlParameter("name", Author.FullName), new SqlParameter("mob", Author.MobileNo), new SqlParameter("fk", Author.Fk_Book_Id));
                db.SaveChanges();
                return RedirectToAction("MyIndex");
            }

            return View(Author);
        }
        /*-----------------------------------
        *  Edit Method
        *  ---------------------------------*/
        //Edit row in database 
        //GET : Author/MyEdit/2
        public ActionResult MyEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var MyID = new SqlParameter("ID", id);
            Author MyAuthor = db.Authors.SqlQuery(@"Select * from Author WHERE Pk_Author_Id = @ID", MyID).Single<Author>();
            if (MyAuthor == null)
            {
                return HttpNotFound();
            }
            return View(MyAuthor);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEdit([Bind(Include = "Pk_Author_Id,FullName,MobileNo,Fk_Book_Id")] Author Author)
        {
            if (ModelState.IsValid)
            {
                int MyNewAuthor = db.Database.ExecuteSqlCommand(@"UPDATE Author SET FullName = @name , MobileNo = @mob, Fk_Book_Id = @fk  WHERE Pk_Author_Id = @id ", new SqlParameter("id", Author.Pk_Author_Id), new SqlParameter("name", Author.FullName), new SqlParameter("mob", Author.MobileNo), new SqlParameter("fk", Author.Fk_Book_Id));
                db.SaveChanges();
                return RedirectToAction("MyIndex");
            }

            return View(Author);
        }
        /*-----------------------------------
        *  Delete Method
        *  ---------------------------------*/
        //Delete row in database 
        //GET : AuthorS/MyDelete/2
        public ActionResult MyDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var MyID = new SqlParameter("ID", id);
            Author MyAuthor = db.Authors.SqlQuery(@"Select * from Author WHERE Pk_Author_Id = @ID", MyID).Single<Author>();
            if (MyAuthor == null)
            {
                return HttpNotFound();
            }
            return View(MyAuthor);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyDelete(int id)
        {
            int MyBook = db.Database.ExecuteSqlCommand(@"DELETE FROM Author WHERE Pk_Author_Id = @id ", new SqlParameter("id", id));
            db.SaveChanges();
            return RedirectToAction("MyIndex");
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
