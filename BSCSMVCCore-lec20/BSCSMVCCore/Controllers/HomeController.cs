using BSCSMVCCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BSCSMVCCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult GetAllUsers()
        {
            //User is not login yet
            //if (true)
            //{
            //    return Redirect("/Home/Login?url=Home/GetAllUsers");
            //}

            //using (ContextClass db = new ContextClass())
            //{ 
            //    var list = db.Users.ToList();

            //}
            //SqlConnection con = new SqlConnection();
            //con.Open();

            var list = new List<string>() {
                "Aqib",
                "Zohaib",
                "Akhtar Lava"
            };
            var listClass = new List<string>()
            {
                "BSCS",
                "BSIT",
                "BSSE"

            };
            ViewBag.Page = "All User";
            List<User> users = new List<User>() {
            new User(){ Email="raeesulislam@pucit.edu.pk" , Name="Raees Ul Islam",Password="123"},
            new User(){ Email="raeesulislam2@pucit.edu.pk" , Name="Raees Ul Islam",Password="123"},
            new User(){ Email="raeesulislam3@pucit.edu.pk" , Name="Raees Ul Islam",Password="123"},
            new User(){ Email="raeesulislam4@pucit.edu.pk" , Name="Raees Ul Islam",Password="123"},
            new User(){ Email="raeesulislam5@pucit.edu.pk" , Name="Raees Ul Islam",Password="123"}
            
            };

            ViewBag.Users = list;
            TempData["Classes"] = listClass;
            ViewData["Temp"] = "Something";
            return View(users);
        }
        public IActionResult Login(string? url)
        {

            //User got logged in
            if (true)
            {
                if (url != null)
                {
                    return Redirect(url);
                }
                else {
                    return Redirect("/Home/Index");
                }
            }
            return View();
        }
    }
}