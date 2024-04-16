using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STORE_APP.Models.Entity;

namespace STORE_APP.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();
        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(CUSTOMERS c)
        {
            if (!ModelState.IsValid)
            {
                return View("Kayit");
            }
            db.CUSTOMERS.Add(c);
            db.SaveChanges();
            return View();
        }

    }
}