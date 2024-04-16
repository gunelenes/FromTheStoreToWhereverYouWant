using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STORE_APP.Models.Entity;
using STORE_APP.Models.Classes;

namespace STORE_APP.Controllers
{
    [Authorize]
    public class Cars_Showcase_Controller : Controller
    {


        // GET: CarsShowcase

        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();

        [HttpGet]
        public ActionResult Index()
        {

            Class1 class1 = new Class1();
            class1.Value1 = db.CARS_CATEGORY.ToList();
            class1.Value2 = db.About.ToList();
            //var car_values = db.CARS_CATEGORY.ToList();

            return View(class1);
        }

        [HttpPost]
        public ActionResult Index(Request p)
        {

            db.Requests.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        [HttpPost]
        public ActionResult CreateAppointment(APPOINTMENT p)
        {
            db.APPOINTMENT.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Cars_Showcase_");
        }
    }
}