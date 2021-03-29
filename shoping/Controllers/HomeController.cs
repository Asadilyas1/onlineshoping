using shoping.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace shoping.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }

        [HttpPost]

        public ActionResult Contact(Contact_us c)
        {
            c.Date_time = System.DateTime.Now;
            db.Contact_us.Add(c);
            db.SaveChanges();

            return View();
        }


        public ActionResult Shop_category()
        {
            return View();
        }

        public ActionResult Index_admin()
        {
            return View();
        }

       

        public ActionResult cart()
        {
            return View();
        }

        public ActionResult shop(int? id)
        {

            TempData["catid"] = id;
            return View();

        }

        [HttpPost]

        public ActionResult shop(string s)
        {
            List<Product> p = new List<Product>();
            if(string.IsNullOrEmpty(s) == false)
            {
                p = db.Products.Where(x => x.Product_Name.StartsWith(s)).ToList();
               
            }
            else
            {
                p = db.Products.ToList();
               
            }
            TempData["searchdata"] = p;
            return View();
        }




        public ActionResult Feedback()
        {
            return View();
        }
        public ActionResult AddtoCart(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Customer_login");
            }
            List<Product> list;

            if(Session["mycart"] ==null)
            { list= new List<Product>(); }
            else
            {
                list = (List<Product>)Session["mycart"];
            }

            Boolean isproductExit = false;
            foreach(var item in list)
            {
                if(id==item.Product_id)
                {
                    isproductExit = true;
                    item.product_quantity++;

                }
            }

            if (isproductExit == false)
            {
               list.Add(db.Products.Where(p => p.Product_id == id).FirstOrDefault());

                list[list.Count - 1].product_quantity = 1;
            }
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
            int p_id = list[RowNO].Product_id;
            int? avaiable = db.Order_details.Where(x => x.Product_Fid == p_id).Sum(x => x.Od_quantity);
            if (avaiable>list[RowNO].product_quantity)

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

        public ActionResult checkout( Order o)
        {
            Account ac = new Account();
            o.Oder_date_time = System.DateTime.Now;
            o.Order_delivery_status = "pending";
            o.Order_status = "Paid";
            o.Order_type = "Sale";
            
                ac = (Account)Session["user"];
           

           
           
            o.Account_Fid = ac.Account_id;
            Session["order"] = o;

            if (o.Order_delivery_status == "Cash on delivery")
            {

                return RedirectToAction("Orderbook");
            }
            else
            {


           return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=asad.ilyas9167890@gmail.com&item_name=onlineshoping&return=https://localhost:44374/Home/Orderbook&amount=" + double.Parse(Session["totalamount"].ToString()) / 160);

            }
            }



        public ActionResult Orderbook()
        {
            Order o= (Order)Session["order"];
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("cookbookand@gmail.com");
            mail.To.Add(o.Order_Email);
            mail.Subject = "order confirmation";
            mail.Body = "<br>Online shoping <br> your order is deliver in 24 hors ";
            mail.IsBodyHtml = true;

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("cookbookand@gmail.com", "AdminATH");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

            //sms code

            // Send SMS to Customer
            String api = "https://lifetimesms.com/json?api_token=56e31a70b238fe3b7d050ba8d1bec8ed4ab8d03132&api_secret=onlinefood&to=" + o.Oder_phone_No + "&from=onlineshoping&message=Order Confirmed by lifetime sms of online shoping for your trust.";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(api);
            var httpResponse = (HttpWebResponse)req.GetResponse();

            //save data

            db.Orders.Add(o);
            db.SaveChanges();


            List<Product> p = (List<Product>)Session["mycart"];

                for (int i = 0; i < p.Count; i++)
                {
                Order_details od = new Order_details();
                int order_id = db.Orders.Max(x => x.Order_id);
                od.Order_Fid = order_id;
                od.Product_Fid = p[i].Product_id;
                od.Od_quantity = p[i].product_quantity*-1;
                od.Od_Prachase_price = p[i].Product_Prachase_price;
                od.Od_Sale_price = p[i].Product_Sale_price;
                db.Order_details.Add(od);
                db.SaveChanges();

                }


              

            return View();
        }

        public ActionResult user_login ()
        {

            return View();
        }
        [HttpPost]

       public ActionResult user_login(Account a)
        {
            a.Account_type = "customer";
            db.Accounts.Add(a);
            db.SaveChanges();

            return RedirectToAction("Customer_login") ;
        }



        public ActionResult Customer_login ()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Customer_login( Account a)
        {

            Account result = db.Accounts.Where(x => x.Account_Email == a.Account_Email && x.Account_password == a.Account_password).FirstOrDefault();

            if (result != null)
            {
                if (result.Account_type == "customer")
                {

                    Session["user"] = result;
                    return RedirectToAction("Shop", "Home");
                }
                else
                {
                    Session["admin"] = result;
                    return RedirectToAction("index_admin", "Home");
                }
            }

            else
            {
                ViewBag.Message = ("Invalid Email & passwords");
                return View();
            }
        }


        public ActionResult Logout()
        {
            Session["admin"] = null;
            return RedirectToAction("Customer_login");
        }


        public ActionResult customerlist()
        {

            return View();
        }

        public ActionResult Close_order()
        {
            Session["mycart"] = null;
            Session["order"] = null;

            return RedirectToAction("Index");
        }

        public ActionResult CustomerLogout ()
        {
            Session["user"] = null;
            return RedirectToAction("Index");

        }


        public ActionResult Order_history ()
        {

            return View();
        }

        public ActionResult invoice(int id)
        {
            var i = db.Orders.Where(x => x.Order_id == id).ToList();
            return View(i);
        }

        public ActionResult Cancel_order(int id)
        {
            Order o = db.Orders.Where(x => x.Order_id == id).FirstOrDefault();
           
            o.Order_delivery_status = "Canceled";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Order_history");
        }

        public ActionResult Cancelorder()
        {
            return View();
        }

        public ActionResult Active(int id)
        {
            Order o = db.Orders.Where(x => x.Order_id == id).FirstOrDefault();
         
            o.Order_delivery_status = "Active";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Order_history");
        }



        public ActionResult Edit_customer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_customer( Account account)
        {
         
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
           
        }

        public ActionResult Customer_Edit_index()
        {
            Account a = (Account)Session["user"];
            var b = db.Accounts.Where(x => x.Account_id == a.Account_id).ToList();

            return View(b);
        }

        public ActionResult Contactus (  )
        {
            var c = db.Contact_us.ToList();
            return View(c);
        }

    }
}