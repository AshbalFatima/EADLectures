using HRM.Models;
using HRM.Repositories.Interfaces;
using HRM.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using HRM.Models.ViewModels;
using System.Globalization;

namespace HRM.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    //[Authorize(Roles = WebsiteRoles.User +","+ WebsiteRoles.Admin)]
    public class PersonalDetailController : Controller
    {
        // GET: PersonalDetailController
        private readonly IEmployeeService _employeSerice;
        private readonly ILOVsService _LOVsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;


        //ForDataForm
        private readonly IAppointmentService _appiontmentService;
        private readonly IServiceHistory _HistoryService;
        private readonly IQualificationSerivce _qualificationService;
        private readonly IBranchService _branchService;


        private ISession Session
        {
            get { return HttpContext.Session; }
        }
        public PersonalDetailController(IEmployeeService employeSerice,
            ILOVsService lOVsService,
            UserManager<ApplicationUser> roleManager,
            IWebHostEnvironment env,
            IAppointmentService appiontmentService,
            IServiceHistory HistoryService,
            IQualificationSerivce qualificationService,
            IBranchService branchService
                )
        {
            _employeSerice = employeSerice;
            _LOVsService = lOVsService;
            _userManager = roleManager;
            _env = env;


            _appiontmentService = appiontmentService;
            _HistoryService = HistoryService;
            _qualificationService = qualificationService;
            _branchService = branchService;
        }

        public ActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var tmp = _employeSerice.GetbyUserId(userId);

            var currentUser = this.User;

            if (userId == null)
            {


                return Redirect("/Identity/Account/Login");

            }
            var user = _userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            if (user==null || !user.OTP_Verify)
            {
                return Redirect("/Employee/PersonalDetail/VerifyMobile");
            }
            if(tmp == null)
                tmp = new HRM.Models.Employee();
            //  BSToastHelper.Add(TempData, new BSToast("Welcome", "Welcome to HR Software"));

            return View(new EmployeeVM(tmp));
        }
        public ActionResult Preview()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var tmp = _employeSerice.GetbyUserId(userId);
            if (userId == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            EmployeeDataVM data = new EmployeeDataVM();
            if (tmp == null)
            {
                tmp = new HRM.Models.Employee();
            }
            data.Employee = new EmployeeVM(tmp);
            data.Appointments = _appiontmentService.GetAppointments(tmp.Id).Select(t => new AppointmentVM(t)).ToList();
            data.ServiceHistories = _HistoryService.GetList(tmp.Id).Select(t => new ServiceHistoryVM(t)).ToList();
            data.Qualifications = _qualificationService.GetQualifications(tmp.Id).Select(t => new QualificationVM(t)).ToList();

            return PartialView("_EmployeeDataForm", data);
        }




        // GET: PersonalDetailController/Edit/5
        public async Task<ActionResult> Edit()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var tmp = await _userManager.GetUserAsync(User);
            if (tmp == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            //
            var temp = _employeSerice.GetbyUserId(userId);

 
            var ddHelper = new DDHelper(_LOVsService);
            ViewBag.Genders = ddHelper.Genders(temp == null ? "" : temp.Gender);
            ViewBag.Domiciles = ddHelper.Domiciles(temp == null ? "" : temp.DomicileId.ToString());
            ViewBag.Religions = ddHelper.Religions(temp == null ? "" : temp.ReligionId.ToString());
            ViewBag.BPS = ddHelper.PayScales(temp == null ? "" : temp.CurrentPayScaleId.ToString());
            ViewBag.Designations = ddHelper.Designations(temp == null ? "" : temp.CurrentDesignationId.ToString());
            ViewBag.Benches = ddHelper.Benches(temp == null ? "" : temp.CurrentPlacePostingBenchId.ToString());

            var data = _branchService.GetAllBranches();

            var ddData = new DDHelper().GetBranches(data, (temp == null ? -1 : temp.CurrentPostingBranchId));
            ViewBag.Branches = ddData;

            // ViewBag.Branches = ddHelper.Branches(temp == null ? "" : temp.CurrentPostingBranchId.ToString());
            ViewBag.Nationalities = ddHelper.Nationalities(temp == null ? "" : temp.NationalityId.ToString());
            ViewBag.DegreeLevels = ddHelper.DegreeLevels(temp == null ? "" : temp.HighestQualificationLevelAtAppointmentId.ToString());
            ViewBag.MaritalStatuses = ddHelper.MaritalStatuses(temp == null ? "" : temp.MaritalStatus.ToString());
            ViewBag.BloodGroups = ddHelper.BloodGroups(temp == null ? "" : temp.BloodGroup.ToString());
            EmployeeVM evm;
            if (temp != null)
            {
                temp.CNIC = tmp.CNIC;
                temp.SelfContactNumber = tmp.PhoneNumber; 
                temp.PersonnelNumber = tmp.PersonnelNumber;
                temp.Email = tmp.Email;
                evm = new EmployeeVM(temp);
            }
            else {
                evm = new EmployeeVM();
                evm.CNIC = tmp.CNIC;
                evm.SelfContactNumber = tmp.PhoneNumber;
                evm.Email = tmp.Email;
                evm.PersonnelNumber = tmp.PersonnelNumber;
            }


            return View(evm);
        }

        // POST: PersonalDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeVM employee, IFormFile? _CNICFRONTURL, IFormFile? _CNICBACKURL, IFormFile? _CURRENTPAYSLIPURL, IFormFile? _PROFILEURL, IFormFile? _DOMICILEURL)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var tmp = await _userManager.GetUserAsync(User);
            if (tmp == null)
            {
                return Redirect("/Identity/Account/Login");
            }

            employee.UserId = userId;
            try
            {
                //var cnicFrontFile = files.Where(t => t.Name == nameof(employee.CNICFrontURL)).FirstOrDefault();
  

                var data = _branchService.GetAllBranches();

                var ddData = new DDHelper().GetBranches(data, (employee == null ? -1 : employee.CurrentPostingBranchId));
                
                ViewBag.Branches = ddData;
                bool ValidDOB = false;
                if (employee.DOB_FOR == null || employee.DOB_FOR != "")
                {

                    string dateString = employee.DOB_FOR;
                    string format = "dd-MMM-yyyy";
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (DateTime.TryParseExact(dateString, format, provider, DateTimeStyles.None, out DateTime dateTime))
                    {
                        ValidDOB = true;
                        DateTime d = DateTime.ParseExact(dateString, format, provider);
                        employee.DOB = d;
                    }
                    else
                    {
                        ModelState.AddModelError("DOB", "Invalid Date");

                    }
                    

                }
                else { ModelState.AddModelError("DOB", "Invalid Date"); }


                if (ModelState.IsValid && ValidDOB)
                {

                    employee.CurrentPayScale = _LOVsService.GetText(employee.CurrentPayScaleId, LOV_Type.BPS);
                    employee.CurrentPlacePostingBench = _LOVsService.GetText(employee.CurrentPlacePostingBenchId, LOV_Type.Benches);
                    employee.Domicile = _LOVsService.GetText(employee.DomicileId, LOV_Type.Domiciles);
                    employee.HighestQualificationLevelAtAppointment = _LOVsService.GetDegreeLevels().Where(t => t.Value == employee.HighestQualificationLevelAtAppointmentId.ToString()).First().Text;
                    employee.Nationality = _LOVsService.GetText(employee.NationalityId, LOV_Type.Nationalities);
                    employee.Religion = _LOVsService.GetText(employee.ReligionId, LOV_Type.Religions);
                    employee.CurrentDesignation = _LOVsService.GetDesignations().Where(t => t.Value.ToString() == employee.CurrentDesignationId.ToString()).First().Text;//GetText(employee.HighestQualificationLevelAtAppointmentId, LOV_Type.);

                    employee.CurrentPostingBranch = data.Where(t => t.Id == employee.CurrentPostingBranchId).Select(t => t.BranchName).FirstOrDefault();
                    

                    bool error = false;
                    if (employee.MaritalStatus.ToUpper().Contains("OTHER"))
                    {
                        error = true;
                        ModelState.AddModelError("MaritalStatusOther", String.Format("The {0} field is required.", "Marital Status Other"));
                    }
                    if (employee.Religion.ToUpper().Contains("OTHER"))
                    {
                        error = true;
                        ModelState.AddModelError("ReligionOther", String.Format("The {0} field is required.", "Religion Other"));
                    }

                    if (employee.Gender.ToUpper().Contains("OTHER"))
                    {
                        error = true;
                        ModelState.AddModelError("GenderOther", String.Format("The {0} field is required.", "Gender Other"));
                    }

                    if (employee.Domicile.ToUpper().Contains("OTHER"))
                    {
                        error = true;
                        ModelState.AddModelError("DomicileOther", String.Format("The {0} field is required.", "Domicile Other"));
                    }

                    if (employee.Nationality.ToUpper().Contains("OTHER"))
                    {
                        error = true;
                        ModelState.AddModelError("NationalityOther", String.Format("The {0} field is required.", "Nationality Other"));
                    }

                    if (employee.CurrentDesignation.ToUpper().Contains("OTHER"))
                    {
                        error = true;
                        ModelState.AddModelError("CurrentDesignationOther", String.Format("The {0} field is required.", "Current Designation Other"));
                    }





                    if (!employee.Id.HasValue)
                    {


                        if (_CNICFRONTURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            var t= new ImageOperations(_env).ImageUpload(_CNICFRONTURL, ref resultStatus, "", "_F", employee.CNIC, FolderHelper.CNIC);
                            if (resultStatus == UploadStatus.Success)
                            {
                                employee.CNICFrontURL =t;
                            }
                            else {
                                error = true;
                                ModelState.AddModelError("", "Error in File Upload (" + resultStatus.ToString() + ")");
                            }
                        }

                        if (_CNICBACKURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            var t= new ImageOperations(_env).ImageUpload(_CNICBACKURL, ref resultStatus, "", "_B", employee.CNIC, FolderHelper.CNIC);
                            if (resultStatus == UploadStatus.Success)
                            {
                                employee.CNICBackURL =t;
                            }
                            else
                            {
                                error = true;
                                ModelState.AddModelError("", "Error in File Upload (" + resultStatus.ToString() + ")");
                            }
                        }
                        if (_CURRENTPAYSLIPURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            var t= new ImageOperations(_env).ImageUpload(_CURRENTPAYSLIPURL, ref resultStatus, "", "_PaySlip", employee.CNIC, FolderHelper.PaySlip);
                            if (resultStatus == UploadStatus.Success)
                            {
                                employee.CurrentPaySlipURL =t;
                            }
                            else
                            {
                                error = true;
                                ModelState.AddModelError("", "Error in File Upload (" + resultStatus.ToString() + ")");
                            }

                        }
                        if (_PROFILEURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            var t= new ImageOperations(_env).ImageUpload(_PROFILEURL, ref resultStatus, "", "", employee.CNIC, FolderHelper.Profile);
                            if (resultStatus == UploadStatus.Success)
                            {
                                employee.ProfileURL =t;
                            }
                            else
                            {
                                error = true;
                                ModelState.AddModelError("", "Error in File Upload (" + resultStatus.ToString() + ")");
                            }

                        }
                        if (_DOMICILEURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            var t= new ImageOperations(_env).ImageUpload(_DOMICILEURL, ref resultStatus, "", "", employee.CNIC, FolderHelper.Domiciles);
                            if (resultStatus == UploadStatus.Success)
                            {
                                employee.DomicileURL =t;
                            }
                            else
                            {
                                error = true;
                                ModelState.AddModelError("", "Error in File Upload (" + resultStatus.ToString() + ")");
                            }

                        }
                        //employee.CurrentDesignation = _LOVsService.GetText(employee.CurrentDesignationId, LOV_Type.Desginations);

                        employee.CreatedOn = DateTime.Now;

                        if (!error)
                        {
                            _employeSerice.Add(employee.ToModel());
                            _employeSerice.Save();
                        }
                    }
                    else {
                        if (_CNICFRONTURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            var t= new ImageOperations(_env).ImageUpload(_CNICFRONTURL, ref resultStatus, "", "_F", employee.CNIC, FolderHelper.CNIC);
                            if (resultStatus == UploadStatus.Success)
                            {
                                employee.CNICFrontURL =t;
                            }
                            else
                            {
                                error = true;
                                ModelState.AddModelError("", "Error in File Upload (" + resultStatus.ToString() + ")");
                            }
                        }
                        if (_CNICBACKURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            var t= new ImageOperations(_env).ImageUpload(_CNICBACKURL, ref resultStatus, "", "_B", employee.CNIC, FolderHelper.CNIC);
                            if (resultStatus == UploadStatus.Success)
                            {
                                employee.CNICBackURL =t;
                            }
                            else
                            {
                                error = true;
                                ModelState.AddModelError("", "Error in File Upload (" + resultStatus.ToString() + ")");
                            }
                        }
                        if (_CURRENTPAYSLIPURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            var t= new ImageOperations(_env).ImageUpload(_CURRENTPAYSLIPURL, ref resultStatus, "", "_PaySlip", employee.CNIC, FolderHelper.PaySlip);
                            if (resultStatus == UploadStatus.Success)
                            {
                                employee.CurrentPaySlipURL =t;
                            }
                            else
                            {
                                error = true;
                                ModelState.AddModelError("", "Error in File Upload (" + resultStatus.ToString() + ")");
                            }
                        }
                        if (_PROFILEURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            var t= new ImageOperations(_env).ImageUpload(_PROFILEURL, ref resultStatus, "", "", employee.CNIC, FolderHelper.Profile);
                            if (resultStatus == UploadStatus.Success)
                            {
                                employee.ProfileURL =t;
                            }
                            else
                            {
                                error = true;
                                ModelState.AddModelError("", "Error in File Upload (" + resultStatus.ToString() + ")");
                            }
                        }
                        if (_DOMICILEURL != null)
                        {
                            UploadStatus resultStatus = UploadStatus.Started;
                            var t= new ImageOperations(_env).ImageUpload(_DOMICILEURL, ref resultStatus, "", "", employee.CNIC, FolderHelper.Domiciles);
                            if (resultStatus == UploadStatus.Success)
                            {
                                employee.DomicileURL = t;
                            }
                            else
                            {
                                error = true;
                                ModelState.AddModelError("", "Error in File Upload (" + resultStatus.ToString() + ")");
                            }

                        }
                        //employee.CurrentDesignation = _LOVsService.GetText(employee.CurrentDesignationId, LOV_Type.Desginations);

                        employee.CurrentPayScale = _LOVsService.GetText(employee.CurrentPayScaleId, LOV_Type.BPS);
                        employee.CurrentPlacePostingBench = _LOVsService.GetText(employee.CurrentPlacePostingBenchId, LOV_Type.Benches);
                        employee.Domicile = _LOVsService.GetText(employee.DomicileId, LOV_Type.Domiciles);
                        employee.HighestQualificationLevelAtAppointment = _LOVsService.GetDegreeLevels().Where(t => t.Value == employee.HighestQualificationLevelAtAppointmentId.ToString()).First().Text;
                        employee.Nationality = _LOVsService.GetText(employee.NationalityId, LOV_Type.Nationalities);
                        employee.Religion = _LOVsService.GetText(employee.ReligionId, LOV_Type.Religions);
                        employee.CurrentDesignation = _LOVsService.GetDesignations().Where(t => t.Value.ToString() == employee.CurrentDesignationId.ToString()).First().Text;//GetText(employee.HighestQualificationLevelAtAppointmentId, LOV_Type.);

                        employee.CurrentPostingBranch = data.Where(t => t.Id == employee.CurrentPostingBranchId).Select(t => t.BranchName).FirstOrDefault();

                        employee.UploadedOn = DateTime.Now;
                        if (!error)
                        {
                            _employeSerice.Update(employee.ToModel());
                            _employeSerice.Save();
                        }
                    }
                    if (!error)
                        return RedirectToAction(nameof(Index));

                }

                //
                var temp = _employeSerice.GetbyUserId(userId);

                var ddHelper = new DDHelper(_LOVsService);
                ViewBag.Genders = ddHelper.Genders(temp == null ? "" : temp.Gender);
                ViewBag.Domiciles = ddHelper.Domiciles(temp == null ? "" : temp.DomicileId.ToString());
                ViewBag.Religions = ddHelper.Religions(temp == null ? "" : temp.ReligionId.ToString());
                ViewBag.BPS = ddHelper.PayScales(temp == null ? "" : temp.CurrentPayScaleId.ToString());
                ViewBag.Designations = ddHelper.Designations(temp == null ? "" : temp.CurrentDesignationId.ToString());
                ViewBag.Benches = ddHelper.Benches(temp == null ? "" : temp.CurrentPlacePostingBenchId.ToString());
                // ViewBag.Branches = ddHelper.Branches(temp == null ? "" : temp.CurrentPostingBranchId.ToString());
                ViewBag.Nationalities = ddHelper.Nationalities(temp == null ? "" : temp.NationalityId.ToString());
                ViewBag.DegreeLevels = ddHelper.DegreeLevels(temp == null ? "" : temp.HighestQualificationLevelAtAppointmentId.ToString());
                ViewBag.MaritalStatuses = ddHelper.MaritalStatuses(temp == null ? "" : temp.MaritalStatus.ToString());
                ViewBag.BloodGroups = ddHelper.BloodGroups(temp == null ? "" : temp.BloodGroup.ToString());
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
                foreach (var item in errors)
                {
                    if (item != null)
                        ModelState.AddModelError("", item);
                }
                return View(employee);



            }
            catch (Exception ex)
            {
                return View(employee);
            }
        }
        public ActionResult VerifyMobile()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> VerifyMobile(string verify)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var tmp = _employeSerice.GetbyUserId(userId);

            var currentUser = this.User;

            if (userId == null)
            {


                return Redirect("/Identity/Account/Login");

            }
            var user = _userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            if (user.OTP == verify.Trim())
            {
                user.OTP_Verify = true;
                user.PhoneNumberConfirmed = true;
                var temp = await _userManager.UpdateAsync(user);
                return Redirect("/Employee/PersonalDetail/Index");
            }
            else {
                ViewBag.Message = "Invalid Code Please try with correct Code!";
            }
            return View();
        }
        [HttpPost]
        public ActionResult Resend()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var tmp = _employeSerice.GetbyUserId(userId);

            var currentUser = this.User;

            if (userId == null)
            {


                return Redirect("/Identity/Account/Login");

            }
            var user = _userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            HttpClient req = new HttpClient();
            var content = req.GetAsync("https://sys.lhc.gov.pk/api/send_sms_custom.php?number=" + user.PhoneNumber + "&message=Your OTP is "+user.OTP).GetAwaiter().GetResult();
            var result = content.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return RedirectToAction(nameof(VerifyMobile));
            //return View();
        }
    }
}
