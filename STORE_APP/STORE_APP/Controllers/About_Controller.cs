using STORE_APP.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STORE_APP.Controllers
{
    public class About_Controller : Controller
    {
        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();
        // GET: About_
        public ActionResult Index(String p)
        {
            var values = from k in db.About select k;
            if (p != null)
            {
                values = values.Where(m => m.ID.Equals(p));
            }
            return View(values.ToList());
            //var values = db.APPOINTMENT.ToList();
            //return View();
        }

        [HttpGet]
        public ActionResult Add_About()
        {
            return View();
        }
        //Add New Departments For Click Button
        [HttpPost]
        public ActionResult Add_About(About value)
        {
            db.About.Add(value);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Delete_About(int id)
        {
            var about = db.About.Find(id);
            db.About.Remove(about);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Update_About_View(int id)
        {
            var about = db.About.Find(id);
            return View("Update_About_View", about);
        }

        //update department
        public ActionResult Update_About(About p)
        {
            var about = db.About.Find(p.ID);
            about.Defination = p.Defination;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}