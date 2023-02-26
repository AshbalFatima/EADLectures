using HRM.Models;
using HRM.Models.ViewModels;
using HRM.Repositories.Interfaces;
using HRM.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace HRM.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class ServiceHistoryController : Controller
    {


        private readonly IEmployeeService _employeSerice;
        private readonly IServiceHistory _HistoryService;
        private readonly ILOVsService _LOVsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public ServiceHistoryController(IEmployeeService employeSerice, IServiceHistory historyService, ILOVsService lOVsService, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _employeSerice = employeSerice;
            _HistoryService = historyService;
            _LOVsService = lOVsService;
            _userManager = userManager;
            _env = env;
        }

        // GET: ServiceHistoryController
        public ActionResult Index()
        {


            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Employee/PersonalDetail/Edit");
            }
            var temp = _HistoryService.GetList(tmp.Id).Select(t=>new ServiceHistoryVM(t));




            return View(temp);
        }

        // GET: ServiceHistoryController/Details/5
        public ActionResult Details(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Employee/PersonalDetail/Edit?message=Please Save Basic Information First&type=warning");
            }
            var tempData =  _HistoryService.GetById(tmp.Id, id);
            if (tempData != null)
            {
                return View(new ServiceHistoryVM(tempData));
            }

            return View();
        }

        // GET: ServiceHistoryController/Create
        public ActionResult Create()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Employee/PersonalDetail/Edit?message=Please Save Basic Information First&type=warning");
            }
            ServiceHistoryVM model = new ServiceHistoryVM() { EmployeeId = tmp.Id };
            
            var ddHelper = new DDHelper(_LOVsService);
            ViewBag.ServiceTypes = ddHelper.Get("", HRM.Models.LOV_Type.ServiceTypes);
            ViewBag.BPS = ddHelper.PayScales("");
            


            return View(model);
        }

        // POST: ServiceHistoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceHistoryVM model,IFormFile? _RELIEVINGURL)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Employee/PersonalDetail/Edit?message=Please Save Basic Information First&type=warning");
            }
            

            var ddHelper = new DDHelper(_LOVsService);
            var serviceTypes = ddHelper.Get(model.ServiceType , HRM.Models.LOV_Type.ServiceTypes);
            var bpses = ddHelper.PayScales(model.PayScale!=null ? model.PayScale:"");
            model.PayScale = bpses.Where(t=>t.Value == model.PayScaleId.ToString() ).Select(t=>t.Text).FirstOrDefault();
            ViewBag.ServiceTypes = serviceTypes;
            ViewBag.BPS = bpses;

            try
            {
                bool ValidDate = false;
                bool ValidDate2 = false;
                
                if (model.From_FOR == null || model.From_FOR != "")
                {

                    string dateString = model.From_FOR;
                    string format = "dd-MMM-yyyy";
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (DateTime.TryParseExact(dateString, format, provider, DateTimeStyles.None, out DateTime dateTime))
                    {
                        ValidDate = true;
                        DateTime d = DateTime.ParseExact(dateString, format, provider);
                        model.From = d;
                    }
                    else
                    {
                        ModelState.AddModelError("From", "Invalid Date");

                    }


                }
                else { ModelState.AddModelError("From", "Invalid Date"); }

                if (model.To_FOR == null || model.To_FOR != "")
                {

                    string dateString = model.To_FOR;
                    string format = "dd-MMM-yyyy";
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (DateTime.TryParseExact(dateString, format, provider, DateTimeStyles.None, out DateTime dateTime))
                    {
                        ValidDate2 = true;
                        DateTime d = DateTime.ParseExact(dateString, format, provider);
                        model.To = d;
                    }
                    else
                    {
                        ModelState.AddModelError("To", "Invalid Date");

                    }


                }
                else { ModelState.AddModelError("To", "Invalid Date"); }

                if (ModelState.IsValid && ValidDate && ValidDate2)
                {
                    
                    if (_RELIEVINGURL != null)
                    {
                        UploadStatus uploadStatus = UploadStatus.Started;
                        model.RelievingURL = new ImageOperations(_env).ImageUpload(_RELIEVINGURL, ref uploadStatus, "", "", null, FolderHelper.Relievings);
                        if (uploadStatus != UploadStatus.Success)
                        {
                            //BSToastHelper.Add(TempData, new BSToast("Relieving Document", "Upload was not successful please upload document again.Error:" + uploadStatus.ToString()));
                            
                        }
                    }
                    _HistoryService.Add(model.ToModel());

                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: ServiceHistoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Employee/PersonalDetail/Edit?message=Please Save Basic Information First&type=warning");
            }
            ServiceHistoryVM model;
           var tempData = _HistoryService.GetById(tmp.Id, id);
            if (tempData == null)
                return Redirect(nameof(Create) + "message=No Data found Please create new one&type=warning");
            model = new ServiceHistoryVM(tempData);
            var ddHelper = new DDHelper(_LOVsService);
            var serviceTypes = ddHelper.Get(model.ServiceType, HRM.Models.LOV_Type.ServiceTypes);
            var bpses = ViewBag.BPS = ddHelper.PayScales(model.PayScale != null ? model.PayScale : "");
            ViewBag.ServiceTypes = serviceTypes;
            ViewBag.BPS = bpses;

      
            return View(model);
        }

        // POST: ServiceHistoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ServiceHistoryVM model,IFormFile? _RELIEVINGURL)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Employee/PersonalDetail/Edit?message=Please Save Basic Information First&type=warning");
            }
            
            var ddHelper = new DDHelper(_LOVsService);
            var serviceTypes = ddHelper.Get(model.ServiceType, HRM.Models.LOV_Type.ServiceTypes);
            var bpses = ddHelper.PayScales(model.PayScale != null ? model.PayScale : "");
            model.PayScale = bpses.Where(t => t.Value == model.PayScaleId.ToString()).Select(t => t.Text).FirstOrDefault();
            ViewBag.ServiceTypes = serviceTypes;
            ViewBag.BPS = bpses;

            try
            {
                if (ModelState.IsValid)
                {
                    if (_RELIEVINGURL != null)
                    {
                        var tempFileName = model.RelievingURL;
                        UploadStatus uploadStatus = UploadStatus.Started;
                        model.RelievingURL = new ImageOperations(_env).ImageUpload(_RELIEVINGURL, ref uploadStatus, "", "", null, FolderHelper.Relievings);
                        if (uploadStatus != UploadStatus.Success)
                        {
                            //BSToastHelper.Add(TempData, new BSToast("Relieving Document", "Upload was not successful please upload document again.Error:" + uploadStatus.ToString()));

                        }
                        else {
                            new ImageOperations(_env).DeleteFile(tempFileName);
                        }
                    }
                    _HistoryService.Update(model.ToModel());
                    return RedirectToAction(nameof(Index) );
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: ServiceHistoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Employee/PersonalDetail/Edit?message=Please Save Basic Information First&type=warning");
            }
            ServiceHistoryVM model;
            var tempData = _HistoryService.GetById(tmp.Id, id);
            if (tempData == null)
                return Redirect(nameof(Create) + "message=No Data found Please create new one&type=warning");
            model = new ServiceHistoryVM(tempData);
            var ddHelper = new DDHelper(_LOVsService);
            var serviceTypes = ddHelper.Get(model.ServiceType, HRM.Models.LOV_Type.ServiceTypes);
            var bpses = ViewBag.BPS = ddHelper.PayScales(model.PayScale != null ? model.PayScale : "");
            ViewBag.ServiceTypes = serviceTypes;
            ViewBag.BPS = bpses;

            return View(model);
        }

        // POST: ServiceHistoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ServiceHistoryVM model)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

                var tmp = _employeSerice.GetbyUserId(userId);
                if (tmp == null)
                {
                    return Redirect("/Employee/PersonalDetail/Edit?message=Please Save Basic Information First&type=warning");
                }
             
                {
                    var temp = _HistoryService.GetById(tmp.Id, id);
                    _HistoryService.Delete(tmp.Id, id);
                    new ImageOperations(_env).DeleteFile(temp.RelievingURL);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult   CreateAjax()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Employee/PersonalDetail/Edit?message=Please Save Basic Information First&type=warning");
            }
            ServiceHistoryVM model = new ServiceHistoryVM() { EmployeeId = tmp.Id };

            var ddHelper = new DDHelper(_LOVsService);
            ViewBag.ServiceTypes = ddHelper.Get("", HRM.Models.LOV_Type.ServiceTypes);
            ViewBag.BPS = ddHelper.PayScales("");

            ViewBag.ServiceHistories = _HistoryService.GetList(tmp.Id).Select(t => new ServiceHistoryVM(t)).ToList();
            return PartialView("_CreateServiceHistory",model);
            //return View(model);
        }
        // POST: ServiceHistoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAjax(ServiceHistoryVM model)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Json(new { error = "true", message = new string[]{ "User Must Logged In!" } });
            }


            var ddHelper = new DDHelper(_LOVsService);
            var serviceTypes = ddHelper.Get(model.ServiceType, HRM.Models.LOV_Type.ServiceTypes);
            var bpses = ddHelper.PayScales(model.PayScale != null ? model.PayScale : "");
            model.PayScale = bpses.Where(t => t.Value == model.PayScaleId.ToString()).Select(t => t.Text).FirstOrDefault();
            ViewBag.ServiceTypes = serviceTypes;
            ViewBag.BPS = bpses;
            ViewBag.ServiceHistories = _HistoryService.GetList(tmp.Id).Select(t => new ServiceHistoryVM(t)).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    var mo = model.ToModel();
                    _HistoryService.Add(mo);

                    return Json(new { error = "false", message = new string[] { "Saved!" }, serviceId = mo });
                }
                else {
                    return Json(new { error = "true", message = new string[] { "Please enter all required Fields!" } });
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { error = "true", message = new string[] { ex.Message } });
            }
        }
    }
}
