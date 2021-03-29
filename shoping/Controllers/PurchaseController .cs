using shoping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace shoping.Controllers
{
    public class purchaseController : Controller
    {
        Model1 db = new Model1();
       

        public ActionResult cart()
        {
            return View();
        }

        public ActionResult Purchas(int? id)
        {
           
            return View();

        }
       
        public ActionResult AddtoCart(int id)
        {
           
            List<Product> list;

            if(Session["mycart"] ==null)
            { list= new List<Product>(); }
            else
            {
                list = (List<Product>)Session["mycart"];
            }


            list.Add(db.Products.Where(p => p.Product_id == id).FirstOrDefault());

            list[list.Count - 1].product_quantity = 1;
            Session["mycart"] = list;

            return RedirectToAction("Cart");
        }
        public ActionResult Minus(int RowNO)
        {
            List<Product> list =  (List<Product>)Session["mycart"];
           

            list[RowNO].product_quantity--;
            if(list[RowNO].product_quantity == 0)
            {
                list.RemoveAt(RowNO);
            }
            Session["mycart"] = list;

            return RedirectToAction("Cart");
        }
        public ActionResult Plus(int RowNO)
        {
            List<Product> list =  (List<Product>)Session["mycart"];
          

            list[RowNO].product_quantity++;
            Session["mycart"] = list;

            return RedirectToAction("Cart");
        }
        public ActionResult Remove(int RowNO)
        {
            List<Product> list =(List<Product>)Session["mycart"];


            list.RemoveAt(RowNO);
            Session["mycart"] = list;

            return RedirectToAction("Cart");
        }

        public ActionResult purchasenow( Order o)
        {
            o.Oder_date_time = System.DateTime.Now;
            o.Order_delivery_status = "devliver";
            o.Order_status = "Paid";
            o.Order_type = "Purchase";
            Account ac=(Account)Session["admin"];
            o.Account_Fid = ac.Account_id;
            Session["order"] = o;
       
            return RedirectToAction("Orderbook");
            }



        public ActionResult Orderbook()
        {
            Order o= (Order)Session["order"];
           
            // save data 

            db.Orders.Add(o);
            db.SaveChanges();


            List<Product> p = (List<Product>)Session["mycart"];

                for (int i = 0; i < p.Count; i++)
                {
                Order_details od = new Order_details();
                int order_id = db.Orders.Max(x => x.Order_id);
                od.Order_Fid = order_id;
                od.Product_Fid = p[i].Product_id;
                od.Od_quantity = p[i].product_quantity;
                od.Od_Prachase_price = p[i].Product_Prachase_price;
                od.Od_Sale_price = p[i].Product_Sale_price;
                db.Order_details.Add(od);
                db.SaveChanges();

                }


              

            return View();
        }

       

        




    }
}