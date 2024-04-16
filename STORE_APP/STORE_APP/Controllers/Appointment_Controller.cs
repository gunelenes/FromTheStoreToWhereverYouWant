using STORE_APP.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STORE_APP.Controllers
{
    public class Appointment_Controller : Controller
    {
        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();
        // GET: Appointment_
        public ActionResult Index(String p)
        {
            var values = from k in db.APPOINTMENT select k;
            if (p != null)
            {
                values = values.Where(m => m.APPOINTMENT_ID.Equals(p));
            }
            return View(values.ToList());
            //var values = db.APPOINTMENT.ToList();
            //return View();

        }

        [HttpGet]
        public ActionResult Add_Appointment()
        {
            //car eklemek için car category ıd alanına car category isimlerini taşı
            List<SelectListItem> listCarCategory = (from i in db.CARS_CATEGORY.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = i.CAR_CATEGORY_NAME,
                                                        Value = i.CAR_CATEGORY_ID.ToString()
                                                    }).ToList();
            //view'e taşı
            ViewBag.listCarCategoryView = listCarCategory;

            return View();
        }
        [HttpPost]
        public ActionResult Add_Appointment(APPOINTMENT appointment)
        {
            var value = db.CARS_CATEGORY.Where(k => k.CAR_CATEGORY_ID == appointment.CARS_CATEGORY.CAR_CATEGORY_ID).FirstOrDefault();
            appointment.CARS_CATEGORY = value;
            if (appointment.ROTATION == null || (appointment.IS_ACTIV != 1 && appointment.IS_ACTIV != 0) || appointment.CLOCK == null )
            {
                //car eklemek için car category ıd alanına car category isimlerini taşı
                List<SelectListItem> listCarCategory = (from i in db.CARS_CATEGORY.ToList()
                                                        select new SelectListItem
                                                        {
                                                            Text = i.CAR_CATEGORY_NAME,
                                                            Value = i.CAR_CATEGORY_ID.ToString()
                                                        }).ToList();
                //view'e taşı
                ViewBag.listCarCategoryView = listCarCategory;
                return View("Add_Appointment");
            }

            db.APPOINTMENT.Add(appointment);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete Car with Id
        public ActionResult Delete_Appointment(int id)
        {
            var appointment = db.APPOINTMENT.Find(id);
            db.APPOINTMENT.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        //connected Update_Car View for find car with id
        public ActionResult Update_Appointment_View(int id)
        {
            var appointment = db.CARS.Find(id);
            List<SelectListItem> listCarCategory = (from i in db.CARS_CATEGORY.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = i.CAR_CATEGORY_NAME,
                                                        Value = i.CAR_CATEGORY_ID.ToString()
                                                    }).ToList();
            ViewBag.listCarCategoryView = listCarCategory;
            return View("Update_Appointment_View", appointment);
        }

        //Update Car
        public ActionResult Update_Appointment(APPOINTMENT p)
        {
            var appointment = db.APPOINTMENT.Find(p.APPOINTMENT_ID);
            var carCategory = db.CARS_CATEGORY.Where(k => k.CAR_CATEGORY_ID == p.CARS_CATEGORY.CAR_CATEGORY_ID).FirstOrDefault();
            if (p.ROTATION == null || (p.IS_ACTIV != 1 && p.IS_ACTIV != 0) || p.CLOCK == null)
            {
                //car eklemek için car category ıd alanına car category isimlerini taşı
                List<SelectListItem> listCarCategory = (from i in db.CARS_CATEGORY.ToList()
                                                        select new SelectListItem
                                                        {
                                                            Text = i.CAR_CATEGORY_NAME,
                                                            Value = i.CAR_CATEGORY_ID.ToString()
                                                        }).ToList();
                //List<SelectListItem> listCustomerCategory = (from i in db.CUSTOMERS.ToList()
                //                                        select new SelectListItem
                //                                        {
                //                                            Text = i.NAME+" "+i.SURNAME,
                //                                            Value = i.CUSTOMER_ID.ToString()
                //                                        }).ToList();
                ////view'e taşı

                //ViewBag.listCustomerCategoryView = listCustomerCategory;
                ViewBag.listCarCategoryView = listCarCategory;
                return View("Update_Appointment_View");
            }
            appointment.CAR_CATEGORY_ID = carCategory.CAR_CATEGORY_ID;
            appointment.ROTATION = p.ROTATION;
            appointment.DATE = p.DATE;
            appointment.CLOCK = p.CLOCK;
            appointment.U_DATE = p.U_DATE;
            appointment.IS_ACTIV = p.IS_ACTIV;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}