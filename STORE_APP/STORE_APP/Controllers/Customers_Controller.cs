using STORE_APP.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STORE_APP.Controllers
{
    public class Customers_Controller : Controller
    {
        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();
        // GET: Customers_
        public ActionResult Index(string p)
        {
            var values = from k in db.CUSTOMERS select k;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(m => m.EMAIL.Contains(p));
            }
            return View(values.ToList());
        }

        [HttpGet]
        public ActionResult Add_Customers()
        {
            return View();
        }
        //Add New Departments For Click Button
        [HttpPost]
        public ActionResult Add_Customers(CUSTOMERS value)
        {
            if (!ModelState.IsValid)
            {
                return View("Add_Customers");
            }
            db.CUSTOMERS.Add(value);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Delete_Customer(int id)
        {
            var department = db.CUSTOMERS.Find(id);
            if(department.IS_ACTIV==0) {
                department.IS_ACTIV = 1;
            }
            else if(department.IS_ACTIV==1) {
                department.IS_ACTIV = 0;
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Update_Customer_View(int id)
        {
            var customer = db.CUSTOMERS.Find(id);
            return View("Update_Customer_View", customer);
        }

        //update department
        public ActionResult Update_Customer(CUSTOMERS p)
        {
            if (p.NAME==null || p.SURNAME == null || p.EMAIL ==null || p.PASSWORD == null || p.PHONE == null)
            {
                return View("Update_Customer_View");
            }
            var customer = db.CUSTOMERS.Find(p.CUSTOMER_ID);
            customer.NAME = p.NAME;
            customer.EMAIL = p.EMAIL; 
            customer.SURNAME = p.SURNAME;
            customer.COMPANY_NAME = p.COMPANY_NAME; 
            customer.PHONE = p.PHONE;
            customer.PASSWORD = p.PASSWORD;
            customer.U_DATE = p.U_DATE;
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Update_Customer_View_For_Customer()
        {
            int customer_id =Convert.ToInt32(Session["customer_id"]);
            var customer = db.CUSTOMERS.Find(customer_id);
            return View("Update_Customer_View_For_Customer", customer);
        }

        //update department
        public ActionResult Update_Customer_For_Customer(CUSTOMERS p)
        {
            if (p.NAME == null || p.SURNAME == null || p.EMAIL == null || p.PASSWORD == null || p.PHONE == null)
            {
                return View("Update_Customer_View_For_Customer");
            }
            var customer = db.CUSTOMERS.Find(p.CUSTOMER_ID);
            customer.NAME = p.NAME;
            customer.EMAIL = p.EMAIL;
            customer.SURNAME = p.SURNAME;
            customer.COMPANY_NAME = p.COMPANY_NAME;
            customer.PHONE = p.PHONE;
            customer.PASSWORD = p.PASSWORD;
            customer.U_DATE = p.U_DATE;
            db.SaveChanges();
            return RedirectToAction("Update_Customer_View_For_Customer");
        }
    }
}