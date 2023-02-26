using AspNetCoreHero.ToastNotification.Abstractions;
using HRM.Models;
using HRM.Models.ViewModels;
using HRM.Repositories.Interfaces;
using HRM.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class DesignationController : Controller
    {
        private readonly ILOVsService _LOVsService;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        
        
        public INotyfService _notifyService { get; }

        public DesignationController(ILOVsService lOVsService, UserManager<ApplicationUser> userManager, IWebHostEnvironment env, INotyfService notyfService)
        {
            _LOVsService = lOVsService;
            _userManager = userManager;
            _env = env;
            _notifyService = notyfService;
        }
        public IActionResult Index()
        {
            var list = _LOVsService.GetDesignationsData().Select(t => new HRM.Models.ViewModels.DesignationVM(t)).ToList();
            return View(list);
        }
        public IActionResult Create()
        {
            var ddHelper = new DDHelper(_LOVsService);
            ViewBag.BPS = ddHelper.PayScales("");

            return View();
        }
        [HttpPost]
        public IActionResult Create(DesignationVM model)
        {
            var ddHelper = new DDHelper(_LOVsService);
            var temp = ddHelper.PayScales(model.BPSID.ToString());
            ViewBag.BPS = temp;
            if (ModelState.IsValid)
            {
            
                model.BPSNAME = temp.Where(t => t.Value == model.BPSID.ToString()).Select(t => t.Text).FirstOrDefault();
                _LOVsService.InsertDesignations(new List<Designation>() { model.ToModel() });
                _notifyService.Success("Designation Saved Successfully");
                return Redirect("/Admin/Designation/Index");
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
            foreach (var item in errors)
            {
                if (item != null)
                    ModelState.AddModelError("", item);
            }

            return View(model);
        }


        public IActionResult Edit(int id)
        {
            var ddHelper = new DDHelper(_LOVsService);
            var Designation = _LOVsService.GetDesignationById(id);
            if (Designation != null)
            {
                ViewBag.BPS = ddHelper.PayScales(Designation.Id.ToString());
                View(new DesignationVM(Designation));
            }
            else {
                _notifyService.Warning("Invalid Request, no designation found!");
                return Redirect("/Admin/Designation/Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(int id, DesignationVM model)
        {
            if (ModelState.IsValid)
            {
                var ddHelper = new DDHelper(_LOVsService);
                var temp = ddHelper.PayScales("");
                ViewBag.BPS = temp;
                model.BPSNAME = temp.Where(t => t.Value == model.BPSID.ToString()).Select(t => t.Text).FirstOrDefault();
                _LOVsService.UpdateDesignation(model.ToModel());
                _notifyService.Success("Designation Updated Successfully");
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
            foreach (var item in errors)
            {
                if (item != null)
                    ModelState.AddModelError("", item);
            }

            return View(model);
        }
        public IActionResult Delete(int id)
        {
            return View();
        }
    }

}
