using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using shoping.Models;

namespace shoping.Controllers
{
    public class Sub_categoryController : Controller
    {
        private Model1 db = new Model1();

        // GET: Sub_category
        public ActionResult Index()
        {
            var sub_category = db.Sub_category.Include(s => s.Category);
            return View(sub_category.ToList());
        }

        // GET: Sub_category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_category sub_category = db.Sub_category.Find(id);
            if (sub_category == null)
            {
                return HttpNotFound();
            }
            return View(sub_category);
        }

        // GET: Sub_category/Create
        public ActionResult Create()
        {
            ViewBag.Category_Fid = new SelectList(db.Categories, "Category_id", "Category_Name");
            return View();
        }

        // POST: Sub_category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sub_category_id,Sub_category_Name,Category_Fid")] Sub_category sub_category)
        {
            if (ModelState.IsValid)
            {
                db.Sub_category.Add(sub_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_Fid = new SelectList(db.Categories, "Category_id", "Category_Name", sub_category.Category_Fid);
            return View(sub_category);
        }

        // GET: Sub_category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_category sub_category = db.Sub_category.Find(id);
            if (sub_category == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Fid = new SelectList(db.Categories, "Category_id", "Category_Name", sub_category.Category_Fid);
            return View(sub_category);
        }

        // POST: Sub_category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sub_category_id,Sub_category_Name,Category_Fid")] Sub_category sub_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Fid = new SelectList(db.Categories, "Category_id", "Category_Name", sub_category.Category_Fid);
            return View(sub_category);
        }

        // GET: Sub_category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_category sub_category = db.Sub_category.Find(id);
            if (sub_category == null)
            {
                return HttpNotFound();
            }
            return View(sub_category);
        }

        // POST: Sub_category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_category sub_category = db.Sub_category.Find(id);
            db.Sub_category.Remove(sub_category);
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
