using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STORE_APP.Models.Entity;

namespace STORE_APP.Controllers
{
    public class Cars_Category_Controller : Controller
    {

        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();
        // GET: Cars_Category_ Table Values List
        public ActionResult Index()
        {
            var values = db.CARS_CATEGORY.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult Add_Category()
        {       
            return View();
        }
        //Add New Car Category For Click Button
        [HttpPost]
        public ActionResult Add_Category(CARS_CATEGORY value)
        {
            if (!ModelState.IsValid)
            {
                return View("Add_Category");
            }
            db.CARS_CATEGORY.Add(value);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        //Delete Car Category with Id
        public ActionResult Delete_Category(int id)
        {
            var category = db.CARS_CATEGORY.Find(id);
            db.CARS_CATEGORY.Remove(category);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        //connected Update_Category View for find category with id
        public ActionResult Update_Category_View(int id)
        {
            var ctg = db.CARS_CATEGORY.Find(id);
            return View("Update_Category_View", ctg);
        }

        //update category
        public ActionResult Update_Category(CARS_CATEGORY p)
        {
            if (!ModelState.IsValid)
            {
                return View("Update_Category_View");
            }
            var ctg = db.CARS_CATEGORY.Find(p.CAR_CATEGORY_ID);
            ctg.CAR_CATEGORY_NAME = p.CAR_CATEGORY_NAME;
            ctg.IS_ACTIV = p.IS_ACTIV;
            ctg.CAR_IMG = p.CAR_IMG;
            ctg.U_DATE  = p.U_DATE;
            db.SaveChanges();
            return RedirectToAction("index");
        }

    }
}