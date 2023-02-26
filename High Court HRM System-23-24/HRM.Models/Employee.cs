using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
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
        [Display(Name = "Domicile Image:")]
        public string? DomicileURL { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        public string Domicile { get; set; }

        public string? DomicileOther { get; set; }

        [Required]
        [Display(Name = "Domicile")]
        public int DomicileId { get; set; }
        [Required]

        [Display(Name = "Gender")]
        public string? Gender { get; set; }


        [Display(Name = "Other(Please specify)")]
        public string? GenderOther { get; set; }

        [Required]
        [Display(Name = "Blood Group")]
        public string? BloodGroup { get; set; }

        public string Nationality { get; set; }
        public string? NationalityOther { get; set; }
        [Required]
        [Display(Name = "Nationality")]
        public int NationalityId { get; set; }

        public string? ReligionOther { get; set; }
        

        public string Religion { get; set; }
        [Required]
        [Display(Name = "Religion")]
        public int ReligionId { get; set; }



        public string? MaritalStatusOther { get; set; }
        [Required]
        [Display(Name = "Marital Status")]
        public string? MaritalStatus { get; set; }
        
        [Required]
        [Display(Name = "Permanent Address")]
        public string? PermanentAddress { get; set; }
        [Required]
        [Display(Name = "Postal Address")]
        public string? PostalAddress { get; set; }

        [Required]
        [Display(Name = "Self Contact No.")]
        public string SelfContactNumber { get; set; }
        [Required]
        [Display(Name = "Emergency Contact No.")]
        public string? EmergencyContactNo { get; set; }
        [Required]
        [Display(Name = "Emergency Contact Name")]
        public string? EmergencContactName { get; set; }
        [Required]
        [Display(Name = "Relation")]
        public string? EmergencContactRelation { get; set; }


        [Required]

        [Display(Name = "Email If Any")]
        public string? Email { get; set; }

        [Display(Name = "Qualification at the time of Appointment")]
        public string? HighestQualificationLevelAtAppointment { get; set; }
        [Required]
        [Display(Name ="Qualification at the time of Appointment")]
        public int HighestQualificationLevelAtAppointmentId { get; set; }
        public bool IsVerified { get; set; }
        public string? VerifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UploadedOn { get; set; }

        /*Office Use*/
        [Display(Name = "Personal File #")]
        public string? PersonalFileNo { get; set; }
        
        public string PersonnelNumber { get; set; }
        [Required]
        [Display(Name = "Current Designation")]
        public int CurrentDesignationId { get; set; }
        public string CurrentDesignation { get; set; }
        public string? CurrentDesignationOther { get; set; }
        [Required]
        [Display(Name = "Current Posting at")]
        public int CurrentPostingBranchId { get; set; }
        public string CurrentPostingBranch { get; set; }
        [Display(Name = "Current Posting at")]
        public int CurrentPlacePostingBenchId { get; set; }

        [Display(Name = "Current Posting at")]
        public string? CurrentPlacePostingBench { get; set; }
        [Display(Name = "Current Payscale")]
        public string CurrentPayScale { get; set; }
        [Required]
        [Display(Name = "Current Payscale")]
        public int CurrentPayScaleId { get; set; }

        [Display(Name = "Salary Slip Image:")]
        public string? CurrentPaySlipURL { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        public ICollection<Qualification> QualificationsAtAppointment { get; set; }
        public ICollection<Qualification> Qualifications { get; set; }

        public ICollection<Appointment> Appoints { get; set; }
        public ICollection<ServiceHistory> ServiceHistories { get; set; }
        public ICollection<CustomNotification> customNotifications { get; set; }


    }
}
