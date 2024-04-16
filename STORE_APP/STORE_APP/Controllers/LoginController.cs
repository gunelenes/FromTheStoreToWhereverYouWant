using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STORE_APP.Models.Entity;
using System.Web.Security;

namespace STORE_APP.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DB_STORE_APPEntities1 db = new DB_STORE_APPEntities1();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(CUSTOMERS c)
        {
            var information = db.CUSTOMERS.FirstOrDefault(x => x.EMAIL == c.EMAIL && x.PASSWORD == c.PASSWORD);
            if (information != null)
            {
                if (information.EMAIL=="admin" && information.PASSWORD=="123")
                {
                    return RedirectToAction("Index", "Cars_/Index");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(information.EMAIL, false);
                    Session["email"] = information.EMAIL.ToString();
                    Session["customer_id"] = information.CUSTOMER_ID.ToString();
                    return RedirectToAction("Index", "Cars_Showcase_");
                }                          
            }
            else
            {
                return View();
            }
        }
    }
}