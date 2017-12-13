using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Map_Relationships.Models;

namespace Map_Relationships.Controllers
{
    public class PassportDetailsController : Controller
    {
        private RelationshipsEntities db = new RelationshipsEntities();

        // GET: PassportDetails
        public ActionResult Index()
        {
            var passportDetails = db.PassportDetails.Include(p => p.Person);
            return View(passportDetails.ToList());
        }

        // GET: PassportDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassportDetail passportDetail = db.PassportDetails.Find(id);
            if (passportDetail == null)
            {
                return HttpNotFound();
            }
            return View(passportDetail);
        }

        // GET: PassportDetails/Create
        public ActionResult Create()
        {
            ViewBag.Fk_Person_Id = new SelectList(db.People, "Pk_Person_Id", "Name");
            return View();
        }

        // POST: PassportDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_Passport_Id,Passport_Number,Fk_Person_Id")] PassportDetail passportDetail)
        {
            if (ModelState.IsValid)
            {
                db.PassportDetails.Add(passportDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fk_Person_Id = new SelectList(db.People, "Pk_Person_Id", "Name", passportDetail.Fk_Person_Id);
            return View(passportDetail);
        }

        // GET: PassportDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassportDetail passportDetail = db.PassportDetails.Find(id);
            if (passportDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_Person_Id = new SelectList(db.People, "Pk_Person_Id", "Name", passportDetail.Fk_Person_Id);
            return View(passportDetail);
        }

        // POST: PassportDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Passport_Id,Passport_Number,Fk_Person_Id")] PassportDetail passportDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passportDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fk_Person_Id = new SelectList(db.People, "Pk_Person_Id", "Name", passportDetail.Fk_Person_Id);
            return View(passportDetail);
        }

        // GET: PassportDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassportDetail passportDetail = db.PassportDetails.Find(id);
            if (passportDetail == null)
            {
                return HttpNotFound();
            }
            return View(passportDetail);
        }

        // POST: PassportDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PassportDetail passportDetail = db.PassportDetails.Find(id);
            db.PassportDetails.Remove(passportDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
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
