using Newtonsoft.Json.Linq;
using STORE_APP.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Linq;


namespace STORE_APP.Controllers
{
    public class Cars_Information_Controller : Controller
    {
        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();
        // GET: Cars_Information_
        public ActionResult Index(String p)
        {

            var values = from k in db.CARS_INFORMATION select k;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(m => m.CAR_NUMBER_PLATE.Contains(p));
            }
            return View(values.ToList());
        }

        [HttpGet]
        public ActionResult Add_Car_Information()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add_Car_Information(CARS_INFORMATION value)
        {
            if (!ModelState.IsValid)
            {
                return View("Add_Car_Information");
            }
            db.CARS_INFORMATION.Add(value);          
            db.SaveChanges();
            return RedirectToAction("index");
        }

        ////Delete Car Category with Id
        //public ActionResult Delete_Car_Information(int id)
        //{
        //    var value = db.CARS_INFORMATION.Find(id);
        //    db.CARS_INFORMATION.Remove(value);
        //    db.SaveChanges();
        //    return RedirectToAction("index");
        //}

        //connected Update_Department View for find department with id
        public ActionResult Update_Car_Information_View(string id)
        {
            var value = db.CARS_INFORMATION.Find(id);
            return View("Update_Car_Information_View", value);
        }

        //update department
        public ActionResult Update_Car_Information(CARS_INFORMATION p)
        {
            if (!ModelState.IsValid)
            {
                return View("Update_Car_Information_View");
            }
            var information = db.CARS_INFORMATION.Find(p.CAR_INFORMATION_ID);
            // string d = Update_Car_Information_View(); ; 
            information.INFORMATION = p.INFORMATION;
            information.CAR_SHASSIS_CODE = p.CAR_SHASSIS_CODE;
            information.CAR_NUMBER_PLATE = p.CAR_NUMBER_PLATE;
            information.LAST_MAINTANCE_DATE = p.LAST_MAINTANCE_DATE;
            information.NEXT_MAINTANCE_DATE = p.NEXT_MAINTANCE_DATE;
            information.IS_ACTIV = p.IS_ACTIV;
            information.KILOMETER = p.KILOMETER;
            information.C_DATE = p.U_DATE;
            
            //department.IS_ACTIV = p.IS_ACTIV;
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Max_Use_Car()
        {
            var query = from cc in db.CARS_CATEGORY
                        join c in db.CARS on cc.CAR_CATEGORY_ID equals c.CAR_CATEGORY_ID
                        join sh in db.SALES_HISTORY on c.CAR_ID equals sh.CAR_ID
                        where cc.IS_ACTIV == 1
                        group sh by cc.CAR_CATEGORY_NAME into g
                        select new MaxUseCarResult
                        {
                            CarCategoryName = g.Key,
                            NumberOfSales = g.Count()
                        };
            var result = query.OrderByDescending(g => g.NumberOfSales).ToList();
            return View("Max_Use_Car",result);
        }

        public class MaxUseCarResult
        {
            public string CarCategoryName { get; set; }
            public int NumberOfSales { get; set; }
        }

        public ActionResult MaintenanceDueCars()
        {
            var currentDate = DateTime.Now;

            var query = from ci in db.CARS_INFORMATION
                        where ci.NEXT_MAINTANCE_DATE <= currentDate && ci.IS_ACTIV == 1
                        select new MaintenanceDueCarViewModel
                        {
                            CarNumberPlate = ci.CAR_NUMBER_PLATE,
                            LastMaintenanceDate = ci.LAST_MAINTANCE_DATE,
                            NextMaintenanceDate = ci.NEXT_MAINTANCE_DATE
                        };

            var result = query.ToList();

            return View("MaintenanceDueCars", result);
        }
        public class MaintenanceDueCarViewModel
        {
            public string CarNumberPlate { get; set; }
            public DateTime? LastMaintenanceDate { get; set; }
            public DateTime? NextMaintenanceDate { get; set; }
        }
    }
}