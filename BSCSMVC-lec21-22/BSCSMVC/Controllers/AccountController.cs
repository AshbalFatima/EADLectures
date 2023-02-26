using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSCSMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction(nameof(Login));
           // return RedirectToAction("Login");
        }
        public ActionResult Login() {

            return View();

        }
        public ActionResult Register()
        {

            return View();
        }
    }
}