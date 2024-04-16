using STORE_APP.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace STORE_APP.Controllers
{
    public class Employees_Controller : Controller
    {
        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();

        // GET: Employees_
        public ActionResult Index(String p)
        {
            var values = from k in db.EMPLOYEES select k;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(m => m.NAME.Contains(p));
            }
            return View(values.ToList());
           
        }

        [HttpGet]
        public ActionResult Add_Employee()
        {
            //car eklemek için car category ıd alanına car category isimlerini taşı
            List<SelectListItem> listCarCategory = (from i in db.DEPARTMENTS.ToList()
                                                    select new SelectListItem
                                                    {
                                                       
                                                        Text = i.DEPARTMENT_NAME,
                                                        Value =i.DEPARTMENT_ID.ToString()
                                                    }).ToList();
            //view'e taşı
            ViewBag.listCarCategoryView = listCarCategory;
            return View();
        }
        [HttpPost]
        public ActionResult Add_Employee(EMPLOYEES employee)
        {
            var value = db.DEPARTMENTS.Where(k => k.DEPARTMENT_ID == employee.DEPARTMENTS.DEPARTMENT_ID).FirstOrDefault();
            employee.DEPARTMENTS = value;
            if (employee.EMAIL ==null || employee.PASSWORD == null || employee.NAME == null || employee.SURNAME == null || employee.IDENTIFY_NUMBER == null ||(employee.IS_ACTIV != 0 && employee.IS_ACTIV !=1))
            {
                List<SelectListItem> listCarCategory = (from i in db.DEPARTMENTS.ToList()
                                                        select new SelectListItem
                                                        {

                                                            Text = i.DEPARTMENT_NAME,
                                                            Value = i.DEPARTMENT_ID.ToString()
                                                        }).ToList();
                //view'e taşı
                ViewBag.listCarCategoryView = listCarCategory;
                return View("Add_Employee");
            }
            employee.TOTAL_SERVICE = 0;
            db.EMPLOYEES.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete Car with Id
        public ActionResult Delete_Employee(int id)
        {
            var employee = db.EMPLOYEES.Find(id);
            db.EMPLOYEES.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        //connected Update_Car View for find car with id
        public ActionResult Update_Employee_View(int id)
        {
            var employee = db.CARS.Find(id);
            //car eklemek için car category ıd alanına car category isimlerini taşı
            List<SelectListItem> listCarCategory = (from i in db.DEPARTMENTS.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = i.DEPARTMENT_NAME,
                                                        Value = i.DEPARTMENT_ID.ToString()
                                                    }).ToList();
            //view'e taşı
            ViewBag.listCarCategoryView = listCarCategory;
            return View("Update_Employee_View", employee);
        }

        //Update Car
        public ActionResult Update_Employee(EMPLOYEES p)
        {
            var employee = db.EMPLOYEES.Find(p.EMPLOYEE_ID);
            var value = db.DEPARTMENTS.Where(k => k.DEPARTMENT_ID == p.DEPARTMENTS.DEPARTMENT_ID).FirstOrDefault();
            if (p.EMAIL == null || p.PASSWORD == null || p.NAME == null || p.SURNAME == null || p.IDENTIFY_NUMBER == null || (p.IS_ACTIV != 0 && p.IS_ACTIV != 1))
            {
                List<SelectListItem> listCarCategory = (from i in db.DEPARTMENTS.ToList()
                                                        select new SelectListItem
                                                        {

                                                            Text = i.DEPARTMENT_NAME,
                                                            Value = i.DEPARTMENT_ID.ToString()
                                                        }).ToList();
                //view'e taşı
                ViewBag.listCarCategoryView = listCarCategory;
                return View("Update_Employee_View");
            }
            employee.DEPARTMENT_ID = value.DEPARTMENT_ID;
            employee.NAME = p.NAME;
            employee.SURNAME = p.SURNAME;
            employee.BIRT_DATE = p.BIRT_DATE;
            employee.IDENTIFY_NUMBER = p.IDENTIFY_NUMBER;
            employee.PASSWORD = p.PASSWORD;
            employee.EMAIL = p.EMAIL;
            employee.IS_ACTIV = p.IS_ACTIV;
            employee.U_DATE = p.U_DATE;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}