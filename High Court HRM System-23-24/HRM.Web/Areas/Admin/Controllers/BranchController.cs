using AspNetCoreHero.ToastNotification.Abstractions;
using HRM.Models;
using HRM.Models.ViewModels;
using HRM.Repositories.Interfaces;
using HRM.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRM.Web.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BranchController : Controller
    {

        // GET: PersonalDetailController
        private readonly IBranchService branchService;
        private readonly ILOVsService _LOVsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public INotyfService _notifyService { get; }
        public BranchController(IBranchService branchService, ILOVsService lOVsService, UserManager<ApplicationUser> userManager, IWebHostEnvironment env,INotyfService notyfService)
        {
         

            this.branchService = branchService;
            _LOVsService = lOVsService;
            _userManager = userManager;
            _env = env;
            _notifyService = notyfService;
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            //var tmp = _userManager.GetUserAsync(User).GetAwaiter().GetResult();//.GetbyUserId(userId);
            //if (tmp.Email.ToLower() != "hrmlhc@gmail.com")
            //{
            //    Redirect("/Identity/Account/Login");
            //}
        }


        //ForDataForm



        public IActionResult Index()
        {
            var t = User.IsInRole("Admin");
            var data = branchService.GetAllBranches();
            var ddData = new DDHelper().GetBranchesOrder(data);

            
            return View(ddData);
        }
        public IActionResult Create()
        {
            
            var data = branchService.GetAllBranches();
            var ddData = new DDHelper().GetBranches(data);

            ViewBag.ParentBranches = ddData;
            return View();
        }
        [HttpPost]
        public IActionResult Create(BranchVM branch)
        {
            if (ModelState.IsValid)
            {
                branchService.Add(branch.ToModel());
                _notifyService.Success("Branch has been Add successfully");
                return Redirect("/Admin/Branch/Index"); ;
            }
            var data = branchService.GetAllBranches();
            var ddData = new DDHelper().GetBranches(data);

            ViewBag.ParentBranches = ddData;
            
            return View(branch);


        }
        public IActionResult UpdateOrder(int id,bool up)
        {

            branchService.UpdateOrder(id,up);
            _notifyService.Success("Branch has been updated successfully");
            return Redirect("/Admin/Branch/Index");
        }
        public IActionResult Edit(int id)
        {

            var data = branchService.GetAllBranches();
            Branch b = branchService.GetBranch(id);
            var ddData = new DDHelper().GetBranches(data , b.ParentBranchId.HasValue?b.ParentBranchId.Value:-1);
        
            if (b != null)
            {
                ViewBag.ParentBranches = ddData;

                return View(new BranchVM(b));
            }
            else
            {
                return Redirect("/Admin/Branch/Index");
            }
            
        }
        [HttpPost]
        public IActionResult Edit(int id, BranchVM branch)
        {
            if (ModelState.IsValid)
            {
                branchService.Update(branch.ToModel());
                _notifyService.Success("Branch has been saved successfully");
                return Redirect("/Admin/Branch/Index");
                return Redirect(nameof(Index));
            }
            Branch b = branchService.GetBranch(id);

            var data = branchService.GetAllBranches();
            var ddData = new DDHelper().GetBranches(data,id);

            ViewBag.ParentBranches = ddData;

            if (b != null)
            {
                ViewBag.ParentBranches = ddData;

                return View(new BranchVM(b));
            }
            else
            {
                _notifyService.Success("Invalid Request");
                return Redirect("/Admin/Branch/Index");
            }


        }
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{

        //    var data = branchService.GetBranch(id);
        //    var ddData = new DDHelper().GetBranches(data, id);

        //    ViewBag.ParentBranches = ddData;
        //    return View(data.To);
        //}
        //[HttpPost]
        //public IActionResult Delete(int id,BranchVM branchVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        branchService.Delete(id);
        //        return Redirect(nameof(Index));
        //    }
   

        //    return View(branchVM);


        //}
    }
}
