using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STORE_APP.Models.Entity;
using System.Web.Services.Description;
using Newtonsoft.Json.Linq;

namespace STORE_APP.Controllers
{
    public class Cars_Controller : Controller
    {

        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();
     

        // GET: Cars_ Table Values List
        public ActionResult Index(String p)
        {

            var values = from k in db.CARS select k;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(m => m.CAR_NUMBER_PLATE.Contains(p));
            }
            return View(values.ToList());
        }

        [HttpGet]
        public ActionResult Add_Car()
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
        public ActionResult Add_Car(CARS car)
        {
            var value = db.CARS_CATEGORY.Where(k => k.CAR_CATEGORY_ID == car.CARS_CATEGORY.CAR_CATEGORY_ID).FirstOrDefault();
            car.CARS_CATEGORY = value;
            if (car.CAR_NUMBER_PLATE == null ||  (car.IS_ACTIV !=1 && car.IS_ACTIV != 0) || car.CAR_SHASSIS_CODE == null || car.CAR_SHASSIS_CODE.Length !=17)
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
                return View("Add_Car");
            }
           
            db.CARS.Add(car);
           
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete Car with Id
        public ActionResult Delete_Car(int id)
        {
            var car = db.CARS.Find(id);
            db.CARS.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //connected Update_Car View for find car with id
        public ActionResult Update_Car_View(int id)
        {
            var car = db.CARS.Find(id);
            List<SelectListItem> listCarCategory = (from i in db.CARS_CATEGORY.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = i.CAR_CATEGORY_NAME,
                                                        Value = i.CAR_CATEGORY_ID.ToString()
                                                    }).ToList();
            ViewBag.listCarCategoryView = listCarCategory;
            return View("Update_Car_View", car);
        }

        //Update Car
        public ActionResult Update_Car(CARS p)
        {
            var car = db.CARS.Find(p.CAR_ID);
            var carCategory = db.CARS_CATEGORY.Where(k => k.CAR_CATEGORY_ID == p.CARS_CATEGORY.CAR_CATEGORY_ID).FirstOrDefault();
            if (p.CAR_NUMBER_PLATE == null || (p.IS_ACTIV != 1 && p.IS_ACTIV != 0) || p.CAR_SHASSIS_CODE == null || p.CAR_SHASSIS_CODE.Length != 17)
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
                return View("Update_Car_View");
            }
            car.CAR_CATEGORY_ID = carCategory.CAR_CATEGORY_ID;
            car.CAR_SHASSIS_CODE = p.CAR_SHASSIS_CODE;
            car.CAR_NUMBER_PLATE = p.CAR_NUMBER_PLATE;
            car.IS_ACTIV = p.IS_ACTIV;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}