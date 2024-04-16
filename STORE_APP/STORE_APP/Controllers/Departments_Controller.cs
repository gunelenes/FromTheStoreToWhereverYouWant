using STORE_APP.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace STORE_APP.Controllers
{
    public class Departments_Controller : Controller
    {
        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();
        // GET: Departments_ Table Values List
        public ActionResult Index()
        {
            var values = db.DEPARTMENTS.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult Add_Departments()
        {
            return View();
        }
        //Add New Departments For Click Button
        [HttpPost]
        public ActionResult Add_Departments(DEPARTMENTS value)
        {
            if (!ModelState.IsValid ) {             
                return View("Add_Departments");
            }
            db.DEPARTMENTS.Add(value);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        //Delete Car Category with Id
        public ActionResult Delete_Department(int id)
        {
            var department = db.DEPARTMENTS.Find(id);
            db.DEPARTMENTS.Remove(department);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        //connected Update_Department View for find department with id
        public ActionResult Update_Department_View(int id)
        {
            var department = db.DEPARTMENTS.Find(id);
            return View("Update_Department_View", department);
        }

        //update department
        public ActionResult Update_Department(DEPARTMENTS p)
        {
            if (!ModelState.IsValid)
            {
                return View("Update_Department_View");
            }
            var department = db.DEPARTMENTS.Find(p.DEPARTMENT_ID);
            department.DEPARTMENT_NAME = p.DEPARTMENT_NAME;
            department.IS_ACTIV = p.IS_ACTIV;
            department.U_DATE = p.U_DATE;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}