using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSCSMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        //Home/Profile
        [ActionName("Profile")]
        public ActionResult GetMyData()
        {
            //Home/GetMyData
            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(BSCSMVC.Models.Inquiry input_model)
        {
            if (ModelState.IsValid)
            {
                //you have stored data into database....
                return Redirect("/Home/Submitted");
            }
            else {
               return View(input_model);
            }
        
        }
        public ActionResult SubmitInquiry(BSCSMVC.Models.Inquiry input_model)
        {
            if (ModelState.IsValid)
            {


            }
            else { 
            
            }    


            return Redirect("/Home/Submitted");

        }

        public ActionResult Submitted()
        {
            return View();
        }
        [NonAction]
        public int GenerateSomething()
        {
            return new Random().Next(1, 10000);
        }
    }
}