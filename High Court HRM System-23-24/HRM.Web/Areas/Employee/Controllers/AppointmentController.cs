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
    public class AppointmentController : Controller
    {


        private readonly IEmployeeService _employeSerice;
        private readonly IAppointmentService _appiontmentService;
        private readonly IServiceHistory _serviceHistory;
        private readonly ILOVsService _LOVsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public AppointmentController(IEmployeeService employeSerice,
            IAppointmentService appiontmentService, 
            ILOVsService lOVsService, UserManager<ApplicationUser> userManager,
            IServiceHistory serviceHistory,
            IWebHostEnvironment env)
        {
            _employeSerice = employeSerice;
            _appiontmentService = appiontmentService;
            _LOVsService = lOVsService;
            _userManager = userManager;
            _serviceHistory = serviceHistory;
            _env = env;
        }


        // GET: ApointmentController
        public ActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            var lst = _appiontmentService.GetAppointments(tmp.Id).Select(t=>new AppointmentVM(t)).ToList();
            
                return View(lst);
        }

        // GET: ApointmentController/Details/5
        public ActionResult Details(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            var lst = _appiontmentService.GetAppointmentById(id,tmp.Id);
            if (lst != null)
                return View(new AppointmentVM(lst));

            return View();
        }

        // GET: ApointmentController/Create
        public ActionResult Create()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            AppointmentVM vm = new AppointmentVM() { EmployeeId = tmp.Id };

            var ddHelper = new DDHelper(_LOVsService);
            ViewBag.ModeOfAppointments = ddHelper.Get("", HRM.Models.LOV_Type.AppointmentModes);
            ViewBag.BPS = ddHelper.PayScales("");
            ViewBag.Designations = ddHelper.Designations("");
            ViewBag.Quotas = ddHelper.Get("", HRM.Models.LOV_Type.Quotas);
            return View(vm);
        }

        // POST: ApointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(AppointmentVM model, IFormFile? _APPIONTMENTLETTERURL)
        {
            try
            {
                var ddHelper = new DDHelper(_LOVsService);
                ViewBag.ModeOfAppointments = ddHelper.Get("",HRM.Models.LOV_Type.AppointmentModes);
                ViewBag.BPS = ddHelper.PayScales( "" );
                var designations = ddHelper.Designations("");
                ViewBag.Designations = designations;
                ViewBag.Quotas = ddHelper.Get("", HRM.Models.LOV_Type.Quotas);
                // = ddHelper.PayScales(temp == null ? "" : temp.CurrentPayScaleId.ToString());
                //= ddHelper.Designations(temp == null ? "" : temp.CurrentDesignationId.ToString());
                bool ValidDate = false;
                bool ValidDate2 = false;
                if (model.DateOfAppointmentLetter_FOR== null || model.DateOfAppointmentLetter_FOR != "")
                {

                    string dateString = model.DateOfAppointmentLetter_FOR;
                    string format = "dd-MMM-yyyy";
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (DateTime.TryParseExact(dateString, format, provider, DateTimeStyles.None, out DateTime dateTime))
                    {
                        ValidDate = true;
                        DateTime d = DateTime.ParseExact(dateString, format, provider);
                        model.DateOfAppointmentLetter = d;
                    }
                    else
                    {
                        ModelState.AddModelError("DateOfAppointmentLetter", "Invalid Date");

                    }


                }
                else { ModelState.AddModelError("DateOfAppointmentLetter", "Invalid Date"); }

                if (model.DateOfChargeAssumption_FOR == null || model.DateOfChargeAssumption_FOR != "")
                {

                    string dateString = model.DateOfChargeAssumption_FOR;
                    string format = "dd-MMM-yyyy";
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (DateTime.TryParseExact(dateString, format, provider, DateTimeStyles.None, out DateTime dateTime))
                    {
                        ValidDate2 = true;
                        DateTime d = DateTime.ParseExact(dateString, format, provider);
                        model.DateOfChargeAssumption = d;
                    }
                    else
                    {
                        ModelState.AddModelError("DateOfChargeAssumption", "Invalid Date");

                    }


                }
                else { ModelState.AddModelError("DateOfChargeAssumption", "Invalid Date"); }


                if (ModelState.IsValid && ValidDate && ValidDate2)
                {

                    model.AppointmentAs = designations.Where(t => t.Value == model.AppointmentAsId.ToString()).Select(t => t.Text).FirstOrDefault();
                    model.AppointmentBPS = _LOVsService.GetText(model.AppointmentBPSId, HRM.Models.LOV_Type.BPS);
                    model.ModeOfAppointment = _LOVsService.GetText(model.ModeOfAppointmentId, HRM.Models.LOV_Type.AppointmentModes);
                    model.Quota = _LOVsService.GetText(model.QuotaId, HRM.Models.LOV_Type.Quotas);

                    bool error = false;
                    if (model.JoinThroughProperChannel  && model.ServiceHistoryId.ToString()=="")
                    {
                        error = true;   
                        ModelState.AddModelError("ServiceHistoryId", "Select Previous Service");
                    }
                    if (model.ModeOfAppointment.ToUpper().Contains("OTHER"))
                    {
                        error = true;
                        ModelState.AddModelError("ModeOfAppointmentOther", String.Format("The {0} field is required.", "Mode Of Appointment Other"));
                    }
                    if (model.Quota.ToUpper().Contains("OTHER"))
                    {
                        error = true;
                        ModelState.AddModelError("QuotaOther", String.Format("The {0} field is required.", "Quota Other"));
                    }




                
                    UploadStatus status = UploadStatus.Started;
                    if (_APPIONTMENTLETTERURL != null && !error)
                    {
                        model.AppiontmentLetterURL = new ImageOperations(_env).ImageUpload(_APPIONTMENTLETTERURL, ref status, "", "", null, FolderHelper.AppointmentLetter);
                        if (status == UploadStatus.Success)
                        {
                            _appiontmentService.Add(model.ToModel());
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        
                        ModelState.AddModelError(nameof(model.AppiontmentLetterURL), "Image Upload Error (" + status + ")");
                        return View(model);
                    }
                    
                    
                    
                    
                }
                else {

                    var errors = ModelState.Select((s) => new {
                        fieldName = s.Key,
                        fieldValue = s.Value.RawValue,
                        fieldMessage = s.Value.Errors.FirstOrDefault()==null ? "The field is required!" : s.Value.Errors.First().ErrorMessage
                    });
                    foreach (var item in errors)
                    {
                        if(item!=null)
                        ModelState.AddModelError(item.fieldName, item.fieldMessage);
                    }

                }
                return View(model);
             
            }
            catch(Exception ex)
            {
                throw ex;
                
            }
        }

        // GET: ApointmentController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

                var tmp = _employeSerice.GetbyUserId(userId);
                if (tmp == null)
                {
                    return Redirect("/Identity/Account/Login");
                }
                AppointmentVM vm = new AppointmentVM(_appiontmentService.GetAppointmentById(id, tmp.Id));  //new AppointmentVM() { EmployeeId = tmp.Id };

                var ddHelper = new DDHelper(_LOVsService);
                ViewBag.ModeOfAppointments = ddHelper.Get(vm.ModeOfAppointmentId.ToString(), HRM.Models.LOV_Type.AppointmentModes);
                ViewBag.BPS = ddHelper.PayScales(vm.AppointmentBPSId.ToString());
                var designations = ddHelper.Designations(vm.AppointmentAsId.ToString()) ;
                ViewBag.Designations = designations;
                ViewBag.Quotas = ddHelper.Get(vm.QuotaId.ToString(), HRM.Models.LOV_Type.Quotas);
                // = ddHelper.PayScales(temp == null ? "" : temp.CurrentPayScaleId.ToString());
                //= ddHelper.Designations(temp == null ? "" : temp.CurrentDesignationId.ToString());


                //if (ModelState.IsValid)
                //{

                //    model.AppointmentAs = designations.Where(t => t.Value == model.AppointmentAsId.ToString()).Select(t => t.Text).FirstOrDefault();
                //    model.AppointmentBPS = _LOVsService.GetText(model.AppointmentBPSId, HRM.Models.LOV_Type.BPS);
                //    model.ModeOfAppointment = _LOVsService.GetText(model.ModeOfAppointmentId, HRM.Models.LOV_Type.AppointmentModes);
                //    model.Quota = _LOVsService.GetText(model.QuotaId, HRM.Models.LOV_Type.Quotas);
                //    UploadStatus status = UploadStatus.Started;
                //    model.AppiontmentLetterURL = new ImageOperations(_env).ImageUpload(_APPIONTMENTLETTERURL, ref status, "", "", null, FolderHelper.AppointmentLetter);
                //    if (status != UploadStatus.Success)
                //    {
                //        _appiontmentService.Add(model.ToModel());
                //        return RedirectToAction(nameof(Index));
                //    }
                //    else
                //    {
                //        ModelState.AddModelError(nameof(model.AppiontmentLetterURL), "Image Upload Error (" + status + ")");
                //        return View(model);
                //    }

                //}
                //else
                //{

                //    var errors = ModelState.Select((s) => new {
                //        fieldName = s.Key,
                //        fieldValue = s.Value.RawValue,
                //        fieldMessage = s.Value.Errors.FirstOrDefault()?.ErrorMessage
                //    });
                //    foreach (var item in errors)
                //    {
                //        ModelState.AddModelError(item.fieldName, item.fieldMessage);
                //    }

                //}
                return View(vm);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        // POST: ApointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AppointmentVM model,IFormFile? _APPIONTMENTLETTERURL)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

                var tmp = _employeSerice.GetbyUserId(userId);
                if (tmp == null)
                {
                    return Redirect("/Identity/Account/Login");
                }
                //AppointmentVM vm = new AppointmentVM(_appiontmentService.GetAppointmentById(id, tmp.Id));  //new AppointmentVM() { EmployeeId = tmp.Id };

                var ddHelper = new DDHelper(_LOVsService);
                ViewBag.ModeOfAppointments = ddHelper.Get(model.ModeOfAppointmentId.ToString(), HRM.Models.LOV_Type.AppointmentModes);
                ViewBag.BPS = ddHelper.PayScales(model.AppointmentBPSId.ToString());
                var designations = ddHelper.Designations(model.AppointmentAsId.ToString());
                ViewBag.Designations = designations;
                ViewBag.Quotas = ddHelper.Get(model.QuotaId.ToString(), HRM.Models.LOV_Type.Quotas);
                // = ddHelper.PayScales(temp == null ? "" : temp.CurrentPayScaleId.ToString());
                //= ddHelper.Designations(temp == null ? "" : temp.CurrentDesignationId.ToString());
                bool ValidDate = false;
                bool ValidDate2 = false;
                if (model.DateOfAppointmentLetter_FOR == null || model.DateOfAppointmentLetter_FOR != "")
                {

                    string dateString = model.DateOfAppointmentLetter_FOR;
                    string format = "dd-MMM-yyyy";
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (DateTime.TryParseExact(dateString, format, provider, DateTimeStyles.None, out DateTime dateTime))
                    {
                        ValidDate = true;
                        DateTime d = DateTime.ParseExact(dateString, format, provider);
                        model.DateOfAppointmentLetter = d;
                    }
                    else
                    {
                        ModelState.AddModelError("DateOfAppointmentLetter", "Invalid Date");

                    }


                }
                else { ModelState.AddModelError("DateOfAppointmentLetter", "Invalid Date"); }

                if (model.DateOfChargeAssumption_FOR == null || model.DateOfChargeAssumption_FOR != "")
                {

                    string dateString = model.DateOfChargeAssumption_FOR;
                    string format = "dd-MMM-yyyy";
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (DateTime.TryParseExact(dateString, format, provider, DateTimeStyles.None, out DateTime dateTime))
                    {
                        ValidDate = true;
                        DateTime d = DateTime.ParseExact(dateString, format, provider);
                        model.DateOfChargeAssumption = d;
                    }
                    else
                    {
                        ModelState.AddModelError("DateOfChargeAssumption", "Invalid Date");

                    }


                }
                else { ModelState.AddModelError("DateOfChargeAssumption", "Invalid Date"); }

                if (ModelState.IsValid)
                {

                    model.AppointmentAs = designations.Where(t => t.Value == model.AppointmentAsId.ToString()).Select(t => t.Text).FirstOrDefault();
                    model.AppointmentBPS = _LOVsService.GetText(model.AppointmentBPSId, HRM.Models.LOV_Type.BPS);
                    model.ModeOfAppointment = _LOVsService.GetText(model.ModeOfAppointmentId, HRM.Models.LOV_Type.AppointmentModes);
                    model.Quota = _LOVsService.GetText(model.QuotaId, HRM.Models.LOV_Type.Quotas);



                    bool error = false;
                    if (model.ModeOfAppointment.ToUpper().Contains("OTHER"))
                    {
                        error = true;
                        ModelState.AddModelError("ModeOfAppointmentOther", String.Format("The {0} field is required.", "Mode Of Appointment Other"));
                    }
                    if (model.Quota.ToUpper().Contains("OTHER"))
                    {
                        error = true;
                        ModelState.AddModelError("QuotaOther", String.Format("The {0} field is required.", "Quota Other"));
                    }




                    UploadStatus status = UploadStatus.Started;
                    if (_APPIONTMENTLETTERURL != null && !error)
                    {
                        var tempPath = model.AppiontmentLetterURL;
                        model.AppiontmentLetterURL = new ImageOperations(_env).ImageUpload(_APPIONTMENTLETTERURL, ref status, "", "", null, FolderHelper.AppointmentLetter);
                        if (status == UploadStatus.Success)
                        {
                            new ImageOperations(_env).DeleteFile(tempPath);
                            _appiontmentService.Add(model.ToModel());
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(model.AppiontmentLetterURL), "Image Upload Error (" + status + ")");
                        return View(model);
                    }

                }
                else
                {

                    var errors = ModelState.Select((s) => new
                    {
                        fieldName = s.Key,
                        fieldValue = s.Value.RawValue,
                        fieldMessage = s.Value.Errors.FirstOrDefault()?.ErrorMessage
                    });
                    foreach (var item in errors)
                    {
                        if(item!=null)
                        ModelState.AddModelError(item.fieldName, item.fieldMessage);
                    }

                }
                return View(model);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        // GET: ApointmentController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            var temp = _appiontmentService.GetAppointmentById(id, tmp.Id);
            if(temp!=null)
                return View(new AppointmentVM( temp));
            return View();
        }

        // POST: ApointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AppointmentVM model)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

                var tmp = _employeSerice.GetbyUserId(userId);
                if (tmp == null)
                {
                    return Redirect("/Identity/Account/Login");
                }
                var fullData = _appiontmentService.GetAppointmentById(model.Id.Value, tmp.Id);  
                var tempPath = fullData.AppiontmentLetterURL;

                _appiontmentService.Delete(model.Id.Value, tmp.Id);
                if (fullData.JoinThroughProperChannel && fullData.ServiceHistoryId.HasValue)
                {

                    _serviceHistory.Delete(tmp.Id, fullData.ServiceHistoryId.Value);   
                }
                new ImageOperations(_env).DeleteFile(tempPath);



                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
     
    }
}
