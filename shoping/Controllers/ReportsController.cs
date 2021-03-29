using shoping.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoping.Controllers
{

    public class ReportsController : Controller
    {
        Model1 db = new Model1();
        // GET: Reports
        public ActionResult Purchase_report(FilterModel fm)
        {

            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s");
            }
            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }

            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_id.ToString(), Text = x.Category_Name }).ToList();
            if (fm.Category == null)
            {

                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_id.ToString(), Text = x.Product_Name + "(" + x.Product_description + ")" }).ToList();

            }
            else

            {
                ViewBag.Product = db.Products.Where(x => x.Sub_Category_Fid == fm.Category).Select(x => new SelectListItem { Value = x.Product_id.ToString(), Text = x.Product_Name + "(" + x.Product_description + ")" }).ToList();
            }

            var od = db.Order_details.Select(x => x.Order_Fid).ToList();
            if (fm.Category != null)
            {
                var p = db.Products.Where(x => x.Sub_Category_Fid == fm.Category).Select(x => x.Product_id).ToList();

                if (fm.Product != null)
                {

                    p = db.Products.Where(x => x.Product_id == fm.Product).Select(x => x.Product_id).ToList();
                }
                od = db.Order_details.Where(x => p.Contains(x.Product_Fid)).Select(x => x.Order_Fid).ToList();
            }


            var sr = db.Orders.Where(x => x.Order_type == "Purchase" & x.Oder_date_time >= fm.DateFrom & x.Oder_date_time <= fm.DateTo & od.Contains(x.Order_id)).OrderByDescending(x => x.Order_id).ToList();

            return View(sr);
        }

        public ActionResult invoice(int id)
        {
            var i = db.Orders.Where(x => x.Order_id == id).ToList();
            return View(i);
        }


        public ActionResult Sale_report(FilterModel fm)
        {

            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s");
            }
            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }

            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_id.ToString(), Text = x.Category_Name }).ToList();
            if (fm.Category == null)
            {

                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_id.ToString(), Text = x.Product_Name + "(" + x.Product_description + ")" }).ToList();

            }
            else

            {
                ViewBag.Product = db.Products.Where(x => x.Sub_Category_Fid == fm.Category).Select(x => new SelectListItem { Value = x.Product_id.ToString(), Text = x.Product_Name + "(" + x.Product_description + ")" }).ToList();
            }

            var od = db.Order_details.Select(x => x.Order_Fid).ToList();
            if (fm.Category != null)
            {
                var p = db.Products.Where(x => x.Sub_Category_Fid == fm.Category).Select(x => x.Product_id).ToList();

                if (fm.Product != null)
                {

                    p = db.Products.Where(x => x.Product_id == fm.Product).Select(x => x.Product_id).ToList();
                }
                od = db.Order_details.Where(x => p.Contains(x.Product_Fid)).Select(x => x.Order_Fid).ToList();
            }


            var sr = db.Orders.Where(x => x.Order_type == "Sale" & x.Oder_date_time >= fm.DateFrom & x.Oder_date_time <= fm.DateTo &x.Order_delivery_status== "devliver" & od.Contains(x.Order_id)).OrderByDescending(x => x.Order_id).ToList();

            return View(sr);
        }
        public ActionResult Panding_report(FilterModel fm)
        {

            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s");
            }
            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }

            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_id.ToString(), Text = x.Category_Name }).ToList();
            if (fm.Category == null)
            {

                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_id.ToString(), Text = x.Product_Name + "(" + x.Product_description + ")" }).ToList();

            }
            else

            {
                ViewBag.Product = db.Products.Where(x => x.Sub_Category_Fid == fm.Category).Select(x => new SelectListItem { Value = x.Product_id.ToString(), Text = x.Product_Name + "(" + x.Product_description + ")" }).ToList();
            }

            var od = db.Order_details.Select(x => x.Order_Fid).ToList();
            if (fm.Category != null)
            {
                var p = db.Products.Where(x => x.Sub_Category_Fid == fm.Category).Select(x => x.Product_id).ToList();

                if (fm.Product != null)
                {

                    p = db.Products.Where(x => x.Product_id == fm.Product).Select(x => x.Product_id).ToList();
                }
                od = db.Order_details.Where(x => p.Contains(x.Product_Fid)).Select(x => x.Order_Fid).ToList();
            }


            var sr = db.Orders.Where(x => x.Order_type == "Sale" & x.Order_delivery_status == "pending" ).ToList();

            return View(sr);
        }

        public ActionResult Sale_purchaseReport(FilterModel fm)
        {

            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s");
            }
            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }

            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_id.ToString(), Text = x.Category_Name }).ToList();
            if (fm.Category == null)
            {

                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_id.ToString(), Text = x.Product_Name + "(" + x.Product_description + ")" }).ToList();

            }
            else

            {
                ViewBag.Product = db.Products.Where(x => x.Sub_Category_Fid == fm.Category).Select(x => new SelectListItem { Value = x.Product_id.ToString(), Text = x.Product_Name + "(" + x.Product_description + ")" }).ToList();
            }

            var od = db.Order_details.Select(x => x.Order_Fid).ToList();
            if (fm.Category != null)
            {
                var p = db.Products.Where(x => x.Sub_Category_Fid == fm.Category).Select(x => x.Product_id).ToList();

                if (fm.Product != null)
                {

                    p = db.Products.Where(x => x.Product_id == fm.Product).Select(x => x.Product_id).ToList();
                }
                od = db.Order_details.Where(x => p.Contains(x.Product_Fid)).Select(x => x.Order_Fid).ToList();
            }


            var sr = db.Orders.Where(x => x.Order_type == "Sale" & x.Oder_date_time >= fm.DateFrom & x.Oder_date_time <= fm.DateTo & od.Contains(x.Order_id)).OrderByDescending(x => x.Order_id).ToList();

            return View(sr);
        }

        public ActionResult Stock(FilterModel fm)
        {
            
            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                fm.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s");
            }

            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_id.ToString(), Text = x.Category_Name }).ToList();
            if (fm.Category == null)
            {

                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_id.ToString(), Text = x.Product_Name + "(" + x.Product_description + ")" }).ToList();

            }
            else

            {
                ViewBag.Product = db.Products.Where(x => x.Sub_Category_Fid == fm.Category).Select(x => new SelectListItem { Value = x.Product_id.ToString(), Text = x.Product_Name + "(" + x.Product_description + ")" }).ToList();
            }

            var p = db.Products.ToList();
            if (fm.Category != null)
            {
                 p = db.Products.Where(x => x.Sub_Category_Fid == fm.Category).ToList();

                if (fm.Product != null)
                {

                    p = db.Products.Where(x => x.Product_id == fm.Product).ToList();
                }
              
            }

            return View(p);
        }
        public ActionResult update(int id)
        {
            Order o=  db.Orders.Where(x => x.Order_id == id).FirstOrDefault();
            o.Order_delivery_status= "deliver";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("panding_Report");
        }
    }
}