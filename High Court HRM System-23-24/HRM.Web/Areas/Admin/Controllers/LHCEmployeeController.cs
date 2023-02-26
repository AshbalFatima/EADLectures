using AspNetCoreHero.ToastNotification.Abstractions;
using HRM.Models;
using HRM.Models.ViewModels;
using HRM.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class LHCEmployeeController : Controller
    {
        private readonly ILHCData _employeeDataService;
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public INotyfService _notifyService { get; }
        public LHCEmployeeController(ILHCData employeeDataService, UserManager<ApplicationUser> userManager, IWebHostEnvironment env, INotyfService notyfService)
        {
            _employeeDataService = employeeDataService;
            _userManager = userManager;
            _env = env;
            _notifyService = notyfService;  
        }

        public IActionResult Index(string CNIC,string PersonalNumber)
        {
            if (!string.IsNullOrEmpty(PersonalNumber) || !string.IsNullOrEmpty(CNIC))
            {
                LHCDataSearch d = new LHCDataSearch();
                d.CNIC = CNIC;
                d.PersonalNumber = PersonalNumber;
                d.SearchedData = _employeeDataService.Get(CNIC, PersonalNumber).Select(t=>new LHCDataVM(t)).ToList();
                return View(d);
            }
            
            return View();
        }
        public IActionResult Edit(int id)
        {
            var data = _employeeDataService.GetById(id);
            if (data == null)
            {
                return Redirect("/Admin/EmployeeData/Index");
            }
            var d = new LHCDataVM(data);
            return View(d);

        }
        [HttpPost]
        public IActionResult Edit(LHCDataVM data)
        {
            if (ModelState.IsValid)
            {
                _employeeDataService.Update(data.ToModel());

                _notifyService.Success("Employee Data updated successfully");
                {
                    return Redirect("/Admin/EmployeeData/Index");
                }

                return View(data);
            }
            else {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
                foreach (var item in errors)
                {
                    if (item != null)
                        ModelState.AddModelError("", item);
                }
          
                return View(data);
            }
        
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(HRM.Models.ViewModels.LHCDataVM model)
        {
            if (ModelState.IsValid)
            {
                var temp = _employeeDataService.Get(model.CNIC, model.MobileNumber).Count>0;
                if (temp)
                {
                    _notifyService.Warning("Employee Record Already Exist !");
                    return View(model);

                }

                _employeeDataService.Insert(model.ToModel());
                
                _notifyService.Success("Employee Record Saved Successfully");
                return Redirect("/Admin/LHCEmployee/Index");
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
            foreach (var item in errors)
            {
                if (item != null)
                    ModelState.AddModelError("", item);
            }
            return View(model);
        }


    }
}
