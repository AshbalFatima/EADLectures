using HRM.Models;
using HRM.Repositories;
using HRM.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HRM.Utilities
{
    public interface IDbIntializer 
    {
         void Intialize();
    }
    public class DbIntializer : IDbIntializer
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;
        private ILOVsService _LOVsService;
        private ILHCData _lhcDataservice;

        public DbIntializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,ILOVsService LOVsService,
            ILHCData lhcDataservice)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _LOVsService = LOVsService;
            _lhcDataservice= lhcDataservice;
        }

        public async void Intialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            if (!_roleManager.RoleExistsAsync(WebsiteRoles.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.User)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.HR)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.AR)).GetAwaiter().GetResult();


                var user = new ApplicationUser()
                {
                    UserName = "rtraees@gmail.com",
                    Email = "rtraees@gmail.com",
                    CNIC = "3630226350735",
                    PersonnelNumber = "31890858",
                    PhoneNumber = "03457294449",
                    OTP = "1234"
                };
                var tmp = _userManager.CreateAsync(user, "123456789").GetAwaiter().GetResult();




                //var store = new UserStore<ApplicationUser>(_context);
                //var manager = new ApplicationUserManager(store);
                //var user = new ApplicationUser() { Email = "informatyka4444@wp.pl", UserName = "informatyka4444@wp.pl" };
                //manager.Create(user, "TestPass44!");


                //_userManager.CreateAsync(new ApplicationUser { },"123456789")
                if (tmp == IdentityResult.Success)
                {
                    //var t=  _context.ApplicationUsers.FirstOrDefault(x => x.Email == "rtraees@gmail.com").;
                  
                    //_context.ChangeTracker.Clear();


                    if (user != null)
                    {
                            //   var appuser = new IdentityUser() { Email = t.Email, UserName = t.UserName };
                        
                        var result=   _userManager.AddToRoleAsync(user, WebsiteRoles.Admin).GetAwaiter().GetResult();
                        
                        Console.WriteLine("User Created ? " + result.Succeeded);
                    }

                    //var temp = GetFile("religions.csv",LOV_Type.Religions);
                    //_LOVsService.Insert(temp);
                    //religions
                    _LOVsService.Insert(new List<LOVs>() {
                    new LOVs(){Text="Muslim",Value="1", OrderBy = 1 ,LOV_Type=LOV_Type.Religions},
                    new LOVs(){Text="Christian",Value="2",OrderBy = 2 ,LOV_Type=LOV_Type.Religions},
                    new LOVs(){Text="Hindu",Value="3",OrderBy = 3 ,LOV_Type=LOV_Type.Religions},
                    new LOVs(){Text="Other",Value="4",OrderBy = 4 ,LOV_Type=LOV_Type.Religions},
                    });


                    //religions
                    _LOVsService.Insert(new List<LOVs>() {
                    new LOVs(){Text="Pakistani",Value="1", OrderBy = 1 ,LOV_Type=LOV_Type.Nationalities},
                    new LOVs(){Text="Other",Value="2",OrderBy = 2 ,LOV_Type=LOV_Type.Nationalities},
                    });

                    //BPS Scales
                    var temp2 = GetBPS();
                    _LOVsService.Insert(temp2);

                    //Gender
                    _LOVsService.InsertDesignations(new List<Designation>() {
                        new Designation(){ BPSID=7, BPSNAME="BPS-07" , DesignationTitle =   "Driver"},
                        new Designation(){ BPSID=7, BPSNAME="BPS-07" , DesignationTitle =   "Duplicating Machine Operator"},
                        new Designation(){ BPSID=7, BPSNAME="BPS-07" , DesignationTitle =   "Library Attendant  "},
                        new Designation(){ BPSID=7, BPSNAME="BPS-07" , DesignationTitle =   "Photostate Machine Operator"},
                        new Designation(){ BPSID=7, BPSNAME="BPS-07" , DesignationTitle =   "Technician"},
                        new Designation(){ BPSID=7, BPSNAME="BPS-07" , DesignationTitle =   "Assistant to Care-taker"},
                        new Designation(){ BPSID=6, BPSNAME="BPS-06" , DesignationTitle =   "Office Attendant I & II to HCJ"},
                        new Designation(){ BPSID=5, BPSNAME="BPS-05" , DesignationTitle =   "Book Binder"},
                        new Designation(){ BPSID=5, BPSNAME="BPS-05" , DesignationTitle =   "Fireman"},
                        new Designation(){ BPSID=5, BPSNAME="BPS-05" , DesignationTitle =   "Helper"},
                        new Designation(){ BPSID=5, BPSNAME="BPS-05" , DesignationTitle =   "Helper Cook "},
                        new Designation(){ BPSID=5, BPSNAME="BPS-05" , DesignationTitle =   "Helper Tandoorchi "},
                        new Designation(){ BPSID=5, BPSNAME="BPS-05" , DesignationTitle =   "Head Mali"},
                        new Designation(){ BPSID=5, BPSNAME="BPS-05" , DesignationTitle =   "Khadim Masjid"},
                        new Designation(){ BPSID=5, BPSNAME="BPS-05" , DesignationTitle =   "Moazzin-cum-Khadim"},
                        new Designation(){ BPSID=5, BPSNAME="BPS-05" , DesignationTitle =   "Office Attendant     "},
                        new Designation(){ BPSID=5, BPSNAME="BPS-05" , DesignationTitle =   "Record Lifter"},
                        new Designation(){ BPSID=3, BPSNAME="BPS-03" , DesignationTitle =   "Bearer/Dish Washer "},
                        new Designation(){ BPSID=3, BPSNAME="BPS-03" , DesignationTitle =   "Bearer/Service or Counter Staff"},
                        new Designation(){ BPSID=3, BPSNAME="BPS-03" , DesignationTitle =   "Bearer/Waiter"},
                        new Designation(){ BPSID=3, BPSNAME="BPS-03" , DesignationTitle =   "Chowkidar/Watchman"},
                        new Designation(){ BPSID=3, BPSNAME="BPS-03" , DesignationTitle =   "Denter-cum-painter"},
                        new Designation(){ BPSID=3, BPSNAME="BPS-03" , DesignationTitle =   "Dusting Coolie"},
                        new Designation(){ BPSID=3, BPSNAME="BPS-03" , DesignationTitle =   "Frashman/Frash"},
                        new Designation(){ BPSID=3, BPSNAME="BPS-03" , DesignationTitle =   "Mali (including Baildar & Usher)"},
                        new Designation(){ BPSID=3, BPSNAME="BPS-03" , DesignationTitle =   "Sweeper/Sanitary Worker"},
                        new Designation(){ BPSID=3, BPSNAME="BPS-03" , DesignationTitle =   "Washerman"},
                        new Designation(){ BPSID=3, BPSNAME="BPS-03" , DesignationTitle =   "Waterman"},
                        new Designation(){ BPSID=14 , BPSNAME="BPS-14" , DesignationTitle="Office Coordinator" },
                        new Designation(){ BPSID=13 , BPSNAME="BPS-13" , DesignationTitle="Graphic Designer" },
                        new Designation(){ BPSID=13 , BPSNAME="BPS-13" , DesignationTitle="Web Associate" },
                    });


                    //Gender
                    _LOVsService.Insert(new List<LOVs>() {
                    new LOVs(){Text="Male",Value="Male", OrderBy = 1 ,LOV_Type=LOV_Type.Genders},
                    new LOVs(){Text="Female",Value="Female",OrderBy = 2 ,LOV_Type=LOV_Type.Genders},
                    new LOVs(){Text="Other",Value="Other",OrderBy = 3 ,LOV_Type=LOV_Type.Genders},
                    });

                    //Martial Status
                    _LOVsService.Insert(new List<LOVs>() {
                    new LOVs(){Text="Single",Value="Single", OrderBy = 1 ,LOV_Type=LOV_Type.MartialStatuses},
                    new LOVs(){Text="Married",Value="Married",OrderBy = 2 ,LOV_Type=LOV_Type.MartialStatuses},
                    new LOVs(){Text="Divorced",Value="Divorced",OrderBy = 3 ,LOV_Type=LOV_Type.MartialStatuses},
                    new LOVs(){Text="Widowed",Value="Widowed",OrderBy = 3 ,LOV_Type=LOV_Type.MartialStatuses},
                    new LOVs(){Text="Other",Value="Other",OrderBy = 3 ,LOV_Type=LOV_Type.MartialStatuses},
                    });

                    //Blood Gruops
                    _LOVsService.Insert(new List<LOVs>() {
                        new LOVs(){ Text="(A +ve)" , Value = "(A +ve)" , OrderBy = 1 , LOV_Type= LOV_Type.BloodGroups},
                        new LOVs(){ Text="(A -ve)" , Value = "(A -ve)" , OrderBy = 2 , LOV_Type= LOV_Type.BloodGroups},
                        new LOVs(){ Text="(B +ve)" , Value = "(B +ve)" , OrderBy = 3 , LOV_Type= LOV_Type.BloodGroups},
                        new LOVs(){ Text="(B -ve)" , Value = "(B -ve)" , OrderBy = 4 , LOV_Type= LOV_Type.BloodGroups},
                        new LOVs(){ Text="(O +ve)" , Value = "(O +ve)" , OrderBy = 5 , LOV_Type= LOV_Type.BloodGroups},
                        new LOVs(){ Text="(O -ve)" , Value = "(O -ve)" , OrderBy = 6 , LOV_Type= LOV_Type.BloodGroups},
                        new LOVs(){ Text="(AB +ve)" , Value = "(AB +ve)" , OrderBy = 7 , LOV_Type= LOV_Type.BloodGroups},
                        new LOVs(){ Text="(AB -ve)" , Value = "(AB -ve)" , OrderBy = 8 , LOV_Type= LOV_Type.BloodGroups },
                      });




                    //Benches
                    _LOVsService.Insert(new List<LOVs>() {
                       new LOVs(){ Text=  "Principal Seat" , Value = "1" , OrderBy = 1, LOV_Type = LOV_Type.Benches},
                       new LOVs(){ Text=  "Multan" , Value = "2" , OrderBy = 2, LOV_Type = LOV_Type.Benches},
                       new LOVs(){ Text=  "Rawalpindi" , Value = "3" , OrderBy = 3, LOV_Type = LOV_Type.Benches},
                       new LOVs(){ Text=  "Bahawalpur" , Value = "4" , OrderBy = 4, LOV_Type = LOV_Type.Benches },
                      });

                    //Branch
                    _LOVsService.Insert(new List<LOVs>() {
                       new LOVs(){ Text=  "IT" , Value = "1" , OrderBy = 1, LOV_Type = LOV_Type.Branches},
                       new LOVs(){ Text=  "Judicial" , Value = "2" , OrderBy = 2, LOV_Type = LOV_Type.Branches},
                       new LOVs(){ Text=  "Writ" , Value = "3" , OrderBy = 3, LOV_Type = LOV_Type.Branches},
                       new LOVs(){ Text=  "Criminal" , Value = "4" , OrderBy = 4, LOV_Type = LOV_Type.Branches },
                       new LOVs(){ Text=  "HR-I" , Value = "5" , OrderBy = 4, LOV_Type = LOV_Type.Branches },
                       new LOVs(){ Text=  "HR-II" , Value = "6" , OrderBy = 4, LOV_Type = LOV_Type.Branches },
                       new LOVs(){ Text=  "HR-III" , Value = "7" , OrderBy = 4, LOV_Type = LOV_Type.Branches },
                       new LOVs(){ Text=  "HR-IV" , Value = "8" , OrderBy = 4, LOV_Type = LOV_Type.Branches },
                      });
 
                    //Qualitifcation Types
                    _LOVsService.Insert(new List<LOVs>() {
                       new LOVs(){ Text=  "Before Joining" , Value = "Before Joining" , OrderBy = 1, LOV_Type = LOV_Type.QualficationTimes},
                       new LOVs(){ Text=  "After Joining" , Value = "After Joining" , OrderBy = 2, LOV_Type = LOV_Type.QualficationTimes},
                        });


                    //Mark Types
                    _LOVsService.Insert(new List<LOVs>() {
                       new LOVs(){ Text=  "CGPA" , Value = "CGPA" , OrderBy = 1, LOV_Type = LOV_Type.MarksTypes},
                       new LOVs(){ Text=  "Marks" , Value = "Marks" , OrderBy = 2, LOV_Type = LOV_Type.MarksTypes},
                       new LOVs(){ Text=  "Division" , Value = "Division" , OrderBy = 2, LOV_Type = LOV_Type.MarksTypes},
                       new LOVs(){ Text=  "Grade" , Value = "Grade" , OrderBy = 2, LOV_Type = LOV_Type.MarksTypes},
                       new LOVs(){ Text=  "Pass" , Value = "Pass" , OrderBy = 2, LOV_Type = LOV_Type.MarksTypes},
                       new LOVs(){ Text=  "Fail" , Value = "Fail" , OrderBy = 2, LOV_Type = LOV_Type.MarksTypes},
                        });


                    //Mark Types
                    _LOVsService.Insert(new List<LOVs>() {
                       new LOVs(){ Text=  "Regular" , Value = "Regular" , OrderBy = 1, LOV_Type = LOV_Type.ServiceTypes},
                       new LOVs(){ Text=  "Contract" , Value = "Contract" , OrderBy = 2, LOV_Type = LOV_Type.ServiceTypes},
                        });


                    //Qouta
                    _LOVsService.Insert(new List<LOVs>() {
                       new LOVs(){ Text=  "Open Merit" , Value = "1" , OrderBy = 1, LOV_Type = LOV_Type.Quotas},
                       new LOVs(){ Text=  "Female Quota" , Value = "2" , OrderBy = 2, LOV_Type = LOV_Type.Quotas},
                       new LOVs(){ Text=  "Special Person Quota" , Value = "3" , OrderBy = 3, LOV_Type = LOV_Type.Quotas},
                       new LOVs(){ Text=  "Minority Quota" , Value = "4" , OrderBy = 3, LOV_Type = LOV_Type.Quotas},
                       new LOVs(){ Text=  "Quota reserved for children of retired/serving Employees of LHC" , Value = "5" , OrderBy = 4, LOV_Type = LOV_Type.Quotas},
                       new LOVs(){ Text=  "Quota reserved for Class-VI Employees of LHC" , Value = "6" , OrderBy = 5, LOV_Type = LOV_Type.Quotas},
                       new LOVs(){ Text=  "Other" , Value = "7" , OrderBy = 6, LOV_Type = LOV_Type.Quotas},
                        });

                    // Mode of Appointment
                    _LOVsService.Insert(new List<LOVs>() {
                       new LOVs(){ Text=  "General Recuritment - PPSC" , Value = "1" , OrderBy = 1, LOV_Type = LOV_Type.AppointmentModes},
                       new LOVs(){ Text=  "General Recuritment - LHC" , Value = "2" , OrderBy = 2, LOV_Type = LOV_Type.AppointmentModes},
                       new LOVs(){ Text=  "General Recuritment - Others" , Value = "3" , OrderBy = 3, LOV_Type = LOV_Type.AppointmentModes},
                       new LOVs(){ Text=  "Rule 17-A of PCS (A&CS) Rules, 1974." , Value = "4" , OrderBy = 3, LOV_Type = LOV_Type.AppointmentModes},
                       new LOVs(){ Text=  "Choice Peon" , Value = "5" , OrderBy = 4, LOV_Type = LOV_Type.AppointmentModes},
                       new LOVs(){ Text=  "In relaxation of Rules," , Value = "6" , OrderBy = 5, LOV_Type = LOV_Type.AppointmentModes},
                       new LOVs(){ Text=  "Other" , Value = "7" , OrderBy = 6, LOV_Type = LOV_Type.AppointmentModes},
                        });

                    var domiciles = GetDomiciles();
                    _LOVsService.Insert(domiciles);




                    var levels = GetLevels();
                    _LOVsService.InsertDegreeLevels(levels);
                    var degrees = GetDegrees();
                    _LOVsService.InsertDegrees(degrees);

                    //dummy data
                    _lhcDataservice.AddRange(new List<LHCData>() { 
                    new LHCData(){CNIC = "1",PersonalNumber="1"  ,MobileNumber="" , FatherName="", FullName=""}  ,
                    new LHCData(){CNIC = "2",PersonalNumber="2"  ,MobileNumber="" , FatherName="", FullName=""}  ,
                    new LHCData(){CNIC = "3",PersonalNumber="3"  ,MobileNumber="" , FatherName="", FullName=""}  ,
                    new LHCData(){CNIC = "1234567891011",PersonalNumber="123456789"  ,MobileNumber="" , FatherName="", FullName=""}  ,
                    new LHCData(){CNIC = "abc",PersonalNumber="abc"    ,MobileNumber="" , FatherName="", FullName=""}
                    });
                        
                }
                else
                {
                    throw new Exception("User Creation Faild due to some reseaon");
                }

            }



        }

        private  List<LOVs> GetBPS()
        {
            List<LOVs> bps = new List<LOVs>();
            for (int i = 1; i < 23; i++)
            {
                bps.Add(new LOVs()
                {
                    OrderBy = i,
                    LOV_Type = LOV_Type.BPS,
                    Text = "BPS-" + (i.ToString("00")),
                    Value = i.ToString()

                }); ;
            }
            return bps;
        }
        private List<LOVs> GetDomiciles()
        {
            List<LOVs> domiciles = new List<LOVs>();

            domiciles.Add(new LOVs() { Value = "1", Text = "Lahore", OrderBy = 1, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "2", Text = "Okara", OrderBy = 2, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "3", Text = "Kasur", OrderBy = 3, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "4", Text = "Sheikhupura", OrderBy = 4, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "5", Text = "Faisalabad", OrderBy = 5, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "6", Text = "Hafizabad", OrderBy = 6, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "7", Text = "Gujrat", OrderBy = 7, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "8", Text = "Gujranwala", OrderBy = 8, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "9", Text = "Pakpattan", OrderBy = 9, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "10", Text = "Sialkot", OrderBy = 10, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "11", Text = "Sargodha", OrderBy = 11, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "12", Text = "Khushab", OrderBy = 12, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "13", Text = "Narowal", OrderBy = 13, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "14", Text = "M.B Din", OrderBy = 14, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "15", Text = "Chiniot", OrderBy = 15, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "16", Text = "Toba Tek Singh", OrderBy = 16, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "17", Text = "Mianwali", OrderBy = 17, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "18", Text = "Bhakkar", OrderBy = 18, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "19", Text = "Jhang", OrderBy = 19, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "20", Text = "Nankana Sahib", OrderBy = 20, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "21", Text = "Multan", OrderBy = 21, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "22", Text = "Khanewal", OrderBy = 22, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "23", Text = "Vehari", OrderBy = 23, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "24", Text = "Sahiwal", OrderBy = 24, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "25", Text = "Rajanpur ", OrderBy = 25, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "26", Text = "Lodhran", OrderBy = 26, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "27", Text = "Layyah", OrderBy = 27, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "28", Text = "D.G Khan", OrderBy = 28, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "29", Text = "Muzaffargarh", OrderBy = 29, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "30", Text = "Rawalpindi", OrderBy = 30, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "31", Text = "Jhelum", OrderBy = 31, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "32", Text = "Attock", OrderBy = 32, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "33", Text = "Chakwal", OrderBy = 33, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "34", Text = "Bahawalpur", OrderBy = 34, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "35", Text = "Rahimyar Khan", OrderBy = 35, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "36", Text = "Bahawalnagar", OrderBy = 36, LOV_Type = LOV_Type.Domiciles });
            domiciles.Add(new LOVs() { Value = "37", Text = "Other", OrderBy = 37, LOV_Type = LOV_Type.Domiciles });

            return domiciles;
        }
        private List<LOVs> GetFile(string path, LOV_Type type)
        {
            var items = new List<LOVs>();
            if (System.IO.File.Exists(""))
            {
                using (System.IO.StreamReader reader=new StreamReader(path))
                {
                    int i = 1;
                    string line = null;
                    while ((line=reader.ReadLine())!=null)
                    {
                        items.Add(new LOVs {
                            LOV_Type = type,
                            Text = line,
                        Value = i + "",OrderBy = i
                        }); ;
                        i++;
                    }

                }
            }
            return items;
        }
        private List<DegreeLevel> GetLevels()
        {
            var levels= new List<DegreeLevel>();
            levels.Add(new DegreeLevel { Level = "Illiterate" });
            levels.Add(new DegreeLevel { Level = "Literate" });
            levels.Add(new DegreeLevel { Level = "Non-Matriculation-Primary" });
            levels.Add(new DegreeLevel { Level = "Non-Matriculation-Middle" });
            levels.Add(new DegreeLevel { Level = "Matriculation" });
            levels.Add(new DegreeLevel { Level = "Intermediate" });
            levels.Add(new DegreeLevel { Level = "Graduation" });
            levels.Add(new DegreeLevel { Level = "Masters" });
            levels.Add(new DegreeLevel { Level = "MPhil" });
            levels.Add(new DegreeLevel { Level = "PhD" });
            return levels;
        }
        private List<DegreeTitle> GetDegrees()
        {
            var degrees = new List<DegreeTitle>();
            degrees.Add(new DegreeTitle() { DegreeLevelId = 1, Title = "Illiterate" });

            degrees.Add(new DegreeTitle() { DegreeLevelId = 4, Title = "Middle" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 3, Title = "Primary" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 5, Title = "Matric" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 5, Title = "O levels" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 6, Title = "F.Sc" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 6, Title = "FA" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 6, Title = "A Levels" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 6, Title = "ICS" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 6, Title = "I.Com" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "B.Sc" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "B.A" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "B.Com" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "B.Ed" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "BCS" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "BBA" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "LL.B" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 8, Title = "M.Sc" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 8, Title = "M.A" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 8, Title = "MBA" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 8, Title = "MCS" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 8, Title = "M.Com" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 8, Title = "LL.M" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 8, Title = "M.Ed" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "BIT" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 8, Title = "MIT" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 9, Title = "MPhil" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 10, Title = "PhD" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 6, Title = "D.Com" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "LL.B(Hons)" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "Doctor of Pharmacy" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "BS(Hons)" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "Other" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 6, Title = "DAE" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 6, Title = "Diploma" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 8, Title = "MPA" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "B.Com(Hons)" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 8, Title = "HRM" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "B - Tech" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 8, Title = "Other" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 2 ,Title = "Read & Write" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 2 ,Title = "Read" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 2 ,Title = "Write" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "B.Sc(Hons)" });
            degrees.Add(new DegreeTitle() { DegreeLevelId = 7, Title = "B - Tech(Hons)" });
            return degrees;

        }
        
    }
}
