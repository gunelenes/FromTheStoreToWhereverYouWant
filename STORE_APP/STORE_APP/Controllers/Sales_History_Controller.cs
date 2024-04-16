using STORE_APP.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STORE_APP.Controllers
{
    public class Sales_History_Controller : Controller
    {
        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();
        // GET: About_
        public ActionResult Index(String p)
        {
            var values = from k in db.SALES_HISTORY select k;
            if (p != null)
            {
                values = values.Where(m => m.SALES_ID.Equals(p));
            }
            return View(values.ToList());
            //var values = db.APPOINTMENT.ToList();
            //return View();
        }




        public ActionResult Index_For_Customer()
        {
            int customer_id =Convert.ToInt32(Session["customer_id"]);
            var values = from k in db.SALES_HISTORY select k;
            
             values = values.Where(m => m.CUSTOMER_ID == (customer_id));
            
            return View(values.ToList());
        }

        [HttpGet]
        public ActionResult Add_Sales_History()
        {
            //car eklemek için car category ıd alanına car category isimlerini taşı
            List<SelectListItem> listEmployeeCategory = (from i in db.EMPLOYEES.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = i.NAME + " " + i.SURNAME,
                                                        Value = i.EMPLOYEE_ID.ToString()
                                                    }).ToList();
            //view'e taşı
            ViewBag.listEmployeeView = listEmployeeCategory;

            //car eklemek için car category ıd alanına car category isimlerini taşı
            List<SelectListItem> listCarCategory = (from i in db.CARS.ToList()
                                                         select new SelectListItem
                                                         {
                                                             Text = i.CAR_NUMBER_PLATE,
                                                             Value = i.CAR_ID.ToString()
                                                         }).ToList();
            //view'e taşı
            ViewBag.listCarView = listCarCategory;
            return View();
        }
        [HttpPost]
        public ActionResult Add_Sales_History(SALES_HISTORY sales)
        {
            var value = db.EMPLOYEES.Where(k => k.EMPLOYEE_ID == sales.EMPLOYEES.EMPLOYEE_ID).FirstOrDefault();
            sales.EMPLOYEES = value;
            var value2 = db.CARS.Where(k => k.CAR_ID == sales.CARS.CAR_ID).FirstOrDefault();
            sales.CARS = value2;
            //if (car.CAR_NUMBER_PLATE == null || (car.IS_ACTIV != 1 && car.IS_ACTIV != 0) || car.CAR_SHASSIS_CODE == null || car.CAR_SHASSIS_CODE.Length != 17)
            //{
            //    //car eklemek için car category ıd alanına car category isimlerini taşı
            //    List<SelectListItem> listCarCategory = (from i in db.CARS_CATEGORY.ToList()
            //                                            select new SelectListItem
            //                                            {
            //                                                Text = i.CAR_CATEGORY_NAME,
            //                                                Value = i.CAR_CATEGORY_ID.ToString()
            //                                            }).ToList();
            //    //view'e taşı
            //    ViewBag.listCarCategoryView = listCarCategory;
            //    return View("Add_Car");
            //}

            db.SALES_HISTORY.Add(sales);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete_Sales_History(int id)
        {
            var sales_history = db.SALES_HISTORY.Find(id);
            db.SALES_HISTORY.Remove(sales_history);
            db.SaveChanges();
            return RedirectToAction("index");
        }



        public ActionResult Update_Sales_History_View(int id)
        {
            var sales_history = db.SALES_HISTORY.Find(id);
            //car eklemek için car category ıd alanına car category isimlerini taşı
            List<SelectListItem> listEmployeeCategory = (from i in db.EMPLOYEES.ToList()
                                                         select new SelectListItem
                                                         {
                                                             Text = i.NAME + " " + i.SURNAME,
                                                             Value = i.EMPLOYEE_ID.ToString()
                                                         }).ToList();
            //view'e taşı
            ViewBag.listEmployeeView = listEmployeeCategory;

            //car eklemek için car category ıd alanına car category isimlerini taşı
            List<SelectListItem> listCarCategory = (from i in db.CARS.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = i.CAR_NUMBER_PLATE,
                                                        Value = i.CAR_ID.ToString()
                                                    }).ToList();
            //view'e taşı
            ViewBag.listCarView = listCarCategory;
            return View("Update_Sales_History_View", sales_history);
        }

        //Update Car
        public ActionResult Update_Sales_History(SALES_HISTORY p)
        {
            var sales_history = db.SALES_HISTORY.Find(p.SALES_ID);
            var value = db.EMPLOYEES.Where(k => k.EMPLOYEE_ID == p.EMPLOYEES.EMPLOYEE_ID).FirstOrDefault();
            var value2 = db.CARS.Where(k => k.CAR_ID == p.CARS.CAR_ID).FirstOrDefault();
            //if (p.CAR_NUMBER_PLATE == null || (p.IS_ACTIV != 1 && p.IS_ACTIV != 0) || p.CAR_SHASSIS_CODE == null || p.CAR_SHASSIS_CODE.Length != 17)
            //{
            //    //car eklemek için car category ıd alanına car category isimlerini taşı
            //    List<SelectListItem> listCarCategory = (from i in db.CARS_CATEGORY.ToList()
            //                                            select new SelectListItem
            //                                            {
            //                                                Text = i.CAR_CATEGORY_NAME,
            //                                                Value = i.CAR_CATEGORY_ID.ToString()
            //                                            }).ToList();
            //    //view'e taşı
            //    ViewBag.listCarCategoryView = listCarCategory;
            //    return View("Update_Car_View");
            //}
            sales_history.CAR_ID = value2.CAR_ID;
            sales_history.EMPLOYEE_ID = value.EMPLOYEE_ID;
            sales_history.CUSTOMER_ID = p.CUSTOMER_ID;
            sales_history.EMPLOYEE_POINT = p.EMPLOYEE_POINT;
            sales_history.ROTATION = p.ROTATION;
            sales_history.U_DATE = p.U_DATE;
            sales_history.IS_ACTIV=p.IS_ACTIV;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}