using HRM.Models;
using HRM.Models.ViewModels;
using HRM.Repositories.Interfaces;
using HRM.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace HRM.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class EducationController : Controller
    {
        private readonly IEmployeeService _employeSerice;
        private readonly IQualificationSerivce _qualificationService;
        private readonly ILOVsService _LOVsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public EducationController(IEmployeeService employeeService,
            IQualificationSerivce qualificationService,
            ILOVsService lOVsService, 
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment env)
        {
            _qualificationService = qualificationService;
            _LOVsService = lOVsService;
            _userManager = userManager;
            _env = env;
            _employeSerice = employeeService;
        }



        // GET: EducationController
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
            var qualifications = _qualificationService.GetQualifications(tmp.Id).Select(t=> new QualificationVM(t)).ToList();
            return View(qualifications);
        }

        // GET: EducationController/Details/5
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
            var temp = _qualificationService.GetQualificationById(id, tmp.Id);
            if (temp != null)
                return View(new QualificationVM(temp));
            return View();
        }

        // GET: EducationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EducationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EducationController/Edit/5
        [HttpGet]
        public async  Task<ActionResult> Edit(int id=0, string type="")
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            var tmp = _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            QualificationVM qualificationVM;
            if (id == 0)
            {
                qualificationVM = new QualificationVM();
                qualificationVM.Id = -1;
            }
            else
            {
                qualificationVM = new QualificationVM(_qualificationService.GetQualificationById(id));
            }
            //qualificationVM.QualificationTime = type;
            qualificationVM.EmployeeId = tmp.Id;
            var ddHelper = new DDHelper(_LOVsService);
            qualificationVM.EmployeeId = tmp.Id;
            ViewBag.DegreeLevels = ddHelper.DegreeLevels(qualificationVM == null ? "" : qualificationVM.DegreeLevelId.ToString());
            ViewBag.Degrees = ddHelper.Degrees(qualificationVM == null ? "" : qualificationVM.DegreeId.ToString() , qualificationVM == null ? -1 : qualificationVM.DegreeLevelId);
            ViewBag.Times = ddHelper.Get(qualificationVM == null ? "" : qualificationVM.QualificationTime, HRM.Models.LOV_Type.QualficationTimes);
            ViewBag.Years = ddHelper.GetYears(qualificationVM == null ? -1 : qualificationVM.YearOfResult);
            ViewBag.MarksTypes = ddHelper.Get(qualificationVM == null ? "" : qualificationVM.MarksType , LOV_Type.MarksTypes);
            return View(qualificationVM);
        }

        // POST: EducationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QualificationVM qualificationVM, IFormFile? _DEGREE_FRONT_URL,IFormFile? _DEGREE_BACK_URL, IFormFile? _NOCURL, int id = 0)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var tmp =  _employeSerice.GetbyUserId(userId);
            if (tmp == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            try
            {
                //var cnicFrontFile = files.Where(t => t.Name == nameof(employee.CNICFrontURL)).FirstOrDefault();
                var ddHelper = new DDHelper(_LOVsService);
                var levels = ddHelper.DegreeLevels(qualificationVM == null ? "" : qualificationVM.DegreeLevelId.ToString());
                ViewBag.DegreeLevels = levels;
                var degrees = ddHelper.Degrees(qualificationVM == null ? "" : qualificationVM.DegreeId.ToString(), qualificationVM == null ? -1 : qualificationVM.DegreeLevelId);
                ViewBag.Degrees = degrees;
                ViewBag.Times = ddHelper.Get(qualificationVM == null ? "" : qualificationVM.QualificationTime, HRM.Models.LOV_Type.QualficationTimes);
                ViewBag.Years = ddHelper.GetYears(qualificationVM == null ? -1 : qualificationVM.YearOfResult);
                ViewBag.MarksTypes = ddHelper.Get(qualificationVM == null ? "" : qualificationVM.MarksType,LOV_Type.MarksTypes);

                if (ModelState.IsValid)
                {
                    var l = levels.Where(t => t.Value == qualificationVM.DegreeLevelId.ToString()).Select(t => t.Text).FirstOrDefault();
                    if (l != null)
                        qualificationVM.DegreeLevel = l;
                    var d = degrees.Where(t => t.Value == qualificationVM.DegreeId.ToString()).Select(g => g.Text).FirstOrDefault();
                    if (d != null)
                        qualificationVM.DegreeName = d;

                    bool error = false;
                    if (qualificationVM.DegreeName.ToUpper().Contains("OTHER") && qualificationVM.DegreeNameOther == null || qualificationVM.DegreeNameOther == "")
                    {
                        error = true;
                        ModelState.AddModelError("DegreeNameOther", "The Other Degree Name field is required.!");
                    }

                    if (qualificationVM.Id.HasValue && qualificationVM.Id<0)
                    {


                        if (_DEGREE_BACK_URL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            qualificationVM.Degree_Back_URL = new ImageOperations(_env).ImageUpload(_DEGREE_BACK_URL, ref resultStatus, "", "_B", null, FolderHelper.Degrees);
                        }
                        if (_DEGREE_FRONT_URL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            qualificationVM.Degree_Front_URL = new ImageOperations(_env).ImageUpload(_DEGREE_FRONT_URL, ref resultStatus, "", "_F", null, FolderHelper.Degrees);
                        }
                        if (_NOCURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            qualificationVM.NOCURL = new ImageOperations(_env).ImageUpload(_NOCURL, ref resultStatus, "", "_F", null, FolderHelper.NOCs);



                        }

                        //qualificationVM.CurrentDesignation = _LOVsService.GetText(qualificationVM.CurrentDesignationId, LOV_Type.Desginations);


                        if(!error)
                        _qualificationService.Add(qualificationVM.ToModel());
                        
                    }
                    else
                    {
                        if (_DEGREE_BACK_URL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            qualificationVM.Degree_Back_URL = new ImageOperations(_env).ImageUpload(_DEGREE_BACK_URL, ref resultStatus, "", "_B", null, FolderHelper.Degrees);
                        }
                        if (_DEGREE_FRONT_URL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            qualificationVM.Degree_Front_URL = new ImageOperations(_env).ImageUpload(_DEGREE_FRONT_URL, ref resultStatus, "", "_F", null, FolderHelper.Degrees);
                        }
                        //qualificationVM.CurrentDesignation = _LOVsService.GetText(qualificationVM.CurrentDesignationId, LOV_Type.Desginations);

                        if (_NOCURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            qualificationVM.NOCURL = new ImageOperations(_env).ImageUpload(_NOCURL, ref resultStatus, "", "_F", null, FolderHelper.NOCs);
                        }
                        if (!error)
                            _qualificationService.Update(qualificationVM.ToModel());
                        
                    }
                    if (!error)
                        return RedirectToAction(nameof(Index));
                    return View(qualificationVM);
                }

                //
                var temp = _employeSerice.GetbyUserId(userId);


                //var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => new {Property = c.c.ErrorMessage)).ToList();
                //foreach (var item in errors)
                //{
                //    ModelState.AddModelError("", item);
                //}
                var errors = ModelState.Select((s) => new {
                    fieldName = s.Key,
                    fieldValue = s.Value.RawValue,
                    fieldMessage = s.Value.Errors.FirstOrDefault()?.ErrorMessage
                });
                foreach (var item in errors)
                {
                    ModelState.AddModelError(item.fieldName, item.fieldMessage);
                }
                return View(qualificationVM);


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        // GET: EducationController/Delete/5
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
            var temp = _qualificationService.GetQualificationById(id,tmp.Id);
            if (tmp == null)
            {
                return Redirect("/Identity/Account/AccessDenied");
            }
            return View(new QualificationVM(temp));
        }

        // POST: EducationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
                _qualificationService.Delete(id, tmp.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public ActionResult GetDegrees(int id)
        {
            var degrees = _LOVsService.GetDegrees(id).Select(t=> new {id=t.Value,name= t.Text});
            var json = JsonSerializer.Serialize(degrees);
            return Ok(json);
        }
    }
}
