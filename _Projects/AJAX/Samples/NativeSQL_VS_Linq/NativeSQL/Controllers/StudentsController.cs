using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NativeSQL.Models;
using System.Net;

namespace NativeSQL.Controllers
{
    public class StudentsController : Controller
    {
        private SchoolDB db = new SchoolDB();
        // GET: Students
        public ActionResult Index()
        {
            var StudentsList = db.Students.SqlQuery("SELECT * FROM Students");
            return View(StudentsList);
        }
        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var St = db.Students.SqlQuery("SELECT * FROM Students Where Id = {0}",id).Single();

            if (St == null)
            {
                return HttpNotFound();
            }

            return View(St);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Age")] Students student)
        {
            if (ModelState.IsValid)
            {
                int Insert = db.Database.ExecuteSqlCommand("INSERT INTO STUDENTS VALUES({0},{1},{2}) ", student.Id, student.Name, student.Age);
                return RedirectToAction("Index");
            }

            return View(student);

        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students student = db.Students.SqlQuery("SELECT * FROM STUDENTS WHERE ID = {0}", id).Single();
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        

        // POST: Students/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Age")] Students student)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand("UPDATE STUDENTS SET Name = {0}, Age = {1} WHERE Id = {2} ", student.Name, student.Age, student.Id);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students student = db.Students.SqlQuery("SELECT * FROM STUDENTS WHERE ID = {0}", id).Single();
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
            
        }

        // POST: Students/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id,Name,Age")] Students student)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand("DELETE FROM STUDENTS WHERE Id = {0} ", student.Id);
                return RedirectToAction("Index");
            }
            return View(student);

        }

       
        public PartialViewResult Search()
        {
            string SearchKeyWord = Request["Search"].ToString();
            ViewBag.SearchKeyWord = Request["Search"];
            var StudentsList = db.Students.SqlQuery("SELECT * FROM Students WHERE Name LIKE '" + SearchKeyWord + "%' " ).ToList();
            return PartialView("Search", StudentsList);
        }
    }
}
