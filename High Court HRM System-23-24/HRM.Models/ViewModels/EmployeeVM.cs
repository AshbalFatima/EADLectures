using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models.ViewModels
{
    public class EmployeeVM
    {
        
            public int? Id { get; set; }

            [Required]
            [Display(Name = "Full Name(as per official record)")]
            public string FullName { get; set; }
            [Required]
            [Display(Name = "Father Name")]
            public string FatherName { get; set; }
            [Required]
            [Display(Name = "CNIC (without dashes)")]
            public string CNIC { get; set; }
            [Display(Name = "CNIC Front Image:")]
            public string? CNICFrontURL { get; set; }
            [Display(Name = "CNIC Back Image:")]
            public string? CNICBackURL { get; set; }
            [Display(Name = "Passport Size Image:")]
            public string? ProfileURL { get; set; }
            [Display(Name = "Domicile Image(at the time of appointment)")]
            public string? DomicileURL { get; set; }
            
            [Display(Name = "Date of Birth")]
            [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
            public DateTime? DOB { get; set; }
            public string? DOB_FOR { get; set; }
        [Display(Name = "Other(Please specify)")]
        public string? DomicileOther { get; set; }

        public string? Domicile { get; set; }
            [Required(ErrorMessage = "Domicile field is required")]
            [Display(Name = "Domicile(at the time of Appointment)")]
            public int DomicileId { get; set; }
            [Required]

            [Display(Name = "Gender")]
            public string? Gender { get; set; }
        

            [Display(Name = "Other(Please specify)")]
            public string? GenderOther { get; set; }

            [Required]
            [Display(Name = "Blood Group")]
            public string? BloodGroup { get; set; }
        [Display(Name = "Other(Please specify)")]
        public string? NationalityOther { get; set; }
        public string? Nationality { get; set; }
            [Required(ErrorMessage = "Nationality field is required")]
            [Display(Name = "Nationality")]
            public int NationalityId { get; set; }



            public string? Religion { get; set; }
            [Required(ErrorMessage = "Religion field is required")]
            [Display(Name = "Religion")]
            public int ReligionId { get; set; }
        [Display(Name = "Other(Please specify)")]
        public string? ReligionOther { get; set; }
        [Display(Name = "Other(Please specify)")]
        public string? MaritalStatusOther { get; set; }

        [Required]
            [Display(Name = "Marital Status")]
            public string? MaritalStatus { get; set; }

            [Required]
            [Display(Name = "Permanent Address")]
            public string? PermanentAddress { get; set; }
            [Required]
            [Display(Name = "Postal Address")]
            public string PostalAddress { get; set; }


            [Required]
            [Display(Name = "Contact No.(Self)")]
            public string SelfContactNumber { get; set; }
    
            [Required]
            [Display(Name = "Contact No.(In case of Emergency)")]
            public string EmergencyContactNo { get; set; }
            [Required]
            [Display(Name = "Emergency Contact's Person Name")]
            public string? EmergencContactName { get; set; }
            [Required]
            [Display(Name = "Relation with Employee?")]
            public string? EmergencContactRelation { get; set; }


            [Required]

            [Display(Name = "Email(If Any)")]
            public string? Email { get; set; }

            [Display(Name = "Qualification at the time of Appointment" )]
            
            public string? HighestQualificationLevelAtAppointment { get; set; }
            [Required(ErrorMessage ="Highest Qualification at the time of Appointment field is required" ,AllowEmptyStrings =false)]

            [Display(Name = "Qualification at the time of Appointment")]
            public int HighestQualificationLevelAtAppointmentId { get; set; }
            public bool? IsVerified { get; set; }
            public string? VerifiedBy { get; set; }
            public DateTime? CreatedOn { get; set; }
            public DateTime? UploadedOn { get; set; }

            /*Office Use*/

            [Display(Name = "Personal File #")]
            public string? PersonalFileNo { get; set; }
            [Required(ErrorMessage = "Current Designation field is required")]
            [Display(Name = "Current Designation")]
            public int CurrentDesignationId { get; set; }
            [Display(Name = "Current Designation")]
            public string? CurrentDesignation { get; set; }
        [Display(Name = "Other(Please specify)")]
        public string? CurrentDesignationOther { get; set; }
        //[Required]
        //[Display(Name = "Currently Posting at Branch")]
        // public int CurrentPostingBranchId { get; set; }

            [Display(Name = "Current Place of Posting")]
            public string? CurrentPostingBranch { get; set; }

            [Required]
            [Display(Name = "Current Place of Posting")]
            public int CurrentPostingBranchId { get; set; }
            
            [Display(Name = "Principal Seat/Bench")]
            public int CurrentPlacePostingBenchId { get; set; }
            [Display(Name = "Principal Seat/Bench")]
            public string? CurrentPlacePostingBench { get; set; }

            public string? CurrentPayScale { get; set; }
            [Required(ErrorMessage = "Current Payscale field is required")]
            [Display(Name = "Current Payscale")]
            public int CurrentPayScaleId { get; set; }

            [Display(Name = "Salary Slip Image:")]
            public string? CurrentPaySlipURL { get; set; }
            [Display(Name ="Personnal/Payslip No")]
            public string PersonnelNumber { get; set; }
            public string? UserId { get; set; }

            public EmployeeVM()
            {

            }
            public EmployeeVM(Employee employee)
            {
                this.BloodGroup = employee.BloodGroup;

                this.CNIC = employee.CNIC;
                this.CNICBackURL = employee.CNICBackURL;
                this.CNICFrontURL = employee.CNICFrontURL;
                this.CreatedOn = employee.CreatedOn;
                this.CurrentDesignation = employee.CurrentDesignation;
                this.CurrentDesignationId = employee.CurrentDesignationId;
                this.CurrentPayScale = employee.CurrentPayScale;
                this.CurrentPayScaleId = employee.CurrentPayScaleId;
                this.CurrentPaySlipURL = employee.CurrentPaySlipURL;
                this.CurrentPlacePostingBench = employee.CurrentPlacePostingBench;
                this.CurrentPlacePostingBenchId = employee.CurrentPlacePostingBenchId;
                this.CurrentPostingBranch = employee.CurrentPostingBranch;
                this.CurrentPostingBranchId = employee.CurrentPostingBranchId   ;
                this.DOB = employee.DOB;

                this.Domicile = employee.Domicile;
                this.DomicileId = employee.DomicileId;
                this.Email = employee.Email;
                this.EmergencContactName = employee.EmergencContactName;
                this.EmergencContactRelation = employee.EmergencContactRelation;
                this.EmergencyContactNo = employee.EmergencyContactNo;
                this.FatherName = employee.FatherName;
                this.FullName = employee.FullName;
                this.Gender = employee.Gender;
                this.HighestQualificationLevelAtAppointment = employee.HighestQualificationLevelAtAppointment;
                this.HighestQualificationLevelAtAppointmentId = employee.HighestQualificationLevelAtAppointmentId;
                this.Id = employee.Id;
                this.IsVerified = employee.IsVerified;
                this.MaritalStatus = employee.MaritalStatus;
                this.Nationality = employee.Nationality;
                this.NationalityId = employee.NationalityId;
                this.PersonalFileNo = employee.PersonalFileNo;
                this.PermanentAddress = employee.PermanentAddress;
                this.PostalAddress = employee.PostalAddress;
                this.ProfileURL = employee.ProfileURL;
                this.Religion = employee.Religion;
                this.ReligionId = employee.ReligionId;
                this.UploadedOn = employee.UploadedOn;
                this.UserId = employee.UserId;
                this.VerifiedBy = employee.VerifiedBy;
                this.SelfContactNumber = employee.SelfContactNumber;
                this.PersonnelNumber = employee.PersonnelNumber;

                this.DomicileURL = employee.DomicileURL;
                this.GenderOther = employee.GenderOther;
                this.ReligionOther = employee.ReligionOther;
                this.DomicileOther = employee.DomicileOther;
                this.NationalityOther = employee.NationalityOther;
                this.CurrentDesignationOther = employee.CurrentDesignationOther;
            this.MaritalStatusOther = employee.MaritalStatusOther;
            }

            //public ApplicationUser ApplicationUser { get; set; }
            //public ICollection<Qualification> QualificationsAtAppointment { get; set; }
            //public ICollection<Qualification> Qualifications { get; set; }

            //public ICollection<Appointment> Appoints { get; set; }
            //public ICollection<ServiceHistory> ServiceHistories { get; set; }
            //public ICollection<CustomNotification> customNotifications { get; set; }

        }
        public static class ExtendEmployeeVM
        {
            public static Employee ToModel(this EmployeeVM e)
            {
                Employee ee = new Employee();
                ee.BloodGroup = e.BloodGroup;

                ee.CNIC = e.CNIC;
                ee.CNICBackURL = e.CNICBackURL;
                ee.CNICFrontURL = e.CNICFrontURL;
                ee.CreatedOn = e.CreatedOn;
                ee.CurrentDesignation = e.CurrentDesignation;
                ee.CurrentDesignationId = e.CurrentDesignationId;
                ee.CurrentPayScale = e.CurrentPayScale;
                ee.CurrentPayScaleId = e.CurrentPayScaleId;
                ee.CurrentPaySlipURL = e.CurrentPaySlipURL;
                ee.CurrentPlacePostingBench = e.CurrentPlacePostingBench;
                ee.CurrentPlacePostingBenchId = e.CurrentPlacePostingBenchId;
                ee.CurrentPostingBranch = e.CurrentPostingBranch;
            ee.CurrentPostingBranchId = e.CurrentPostingBranchId;
                ee.DOB = e.DOB ?? e.DOB.Value;
                ee.SelfContactNumber = e.SelfContactNumber;
                ee.Domicile = e.Domicile;
                ee.DomicileId = e.DomicileId;
                ee.Email = e.Email;
                ee.EmergencContactName = e.EmergencContactName;
                ee.EmergencContactRelation = e.EmergencContactRelation;
                ee.EmergencyContactNo = e.EmergencyContactNo;
                ee.FatherName = e.FatherName;
                ee.FullName = e.FullName;
                ee.Gender = e.Gender;
                ee.HighestQualificationLevelAtAppointment = e.HighestQualificationLevelAtAppointment;
                ee.HighestQualificationLevelAtAppointmentId = e.HighestQualificationLevelAtAppointmentId;

                if (e.Id.HasValue)
                    ee.Id = e.Id.Value;

                if (e.IsVerified.HasValue)
                    ee.IsVerified = e.IsVerified.Value;

                ee.MaritalStatus = e.MaritalStatus;
                ee.Nationality = e.Nationality;
                ee.NationalityId = e.NationalityId;
                ee.PersonalFileNo = e.PersonalFileNo;
                ee.PermanentAddress = e.PermanentAddress;
                ee.PostalAddress = e.PostalAddress;
                ee.ProfileURL = e.ProfileURL;
                ee.Religion = e.Religion;
                ee.ReligionId = e.ReligionId;
                ee.UploadedOn = e.UploadedOn;
                ee.UserId = e.UserId;
                ee.VerifiedBy = e.VerifiedBy;
                ee.PersonnelNumber = e.PersonnelNumber;
                ee.DomicileURL = e.DomicileURL;

            ee.GenderOther = e.GenderOther;
            ee.ReligionOther = e.ReligionOther;
            ee.MaritalStatusOther = e.MaritalStatusOther;

                return ee;
            }
        }

    
}
