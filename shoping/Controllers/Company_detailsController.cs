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
    public class Company_detailsController : Controller
    {
        private Model2 db = new Model2();

        // GET: Company_details
        public ActionResult Index()
        {
            return View(db.Company_details.ToList());
        }

        // GET: Company_details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_details company_details = db.Company_details.Find(id);
            if (company_details == null)
            {
                return HttpNotFound();
            }
            return View(company_details);
        }

        // GET: Company_details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company_details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Company_details company_details)
        {
            if (ModelState.IsValid)
            {
                company_details.picture.SaveAs(Server.MapPath("~/Content/Product-pic/" + company_details.picture.FileName));
                company_details.Company_logo = "~/Content/Product-pic/" + company_details.picture.FileName;
                db.Company_details.Add(company_details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company_details);
        }

        // GET: Company_details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_details company_details = db.Company_details.Find(id);
            if (company_details == null)
            {
                return HttpNotFound();
            }
            return View(company_details);
        }

        // POST: Company_details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Company_details company_details)
        {
            if (ModelState.IsValid)
            {
                if (company_details.picture != null)
                {


                    company_details.picture.SaveAs(Server.MapPath("/Content/Product-pic/" + company_details.picture.FileName));
                    company_details.Company_logo = "/Content/Product-pic/" + company_details.picture.FileName;
                }

                db.Entry(company_details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company_details);
        }

        // GET: Company_details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company_details company_details = db.Company_details.Find(id);
            if (company_details == null)
            {
                return HttpNotFound();
            }
            return View(company_details);
        }

        // POST: Company_details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company_details company_details = db.Company_details.Find(id);
            db.Company_details.Remove(company_details);
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
