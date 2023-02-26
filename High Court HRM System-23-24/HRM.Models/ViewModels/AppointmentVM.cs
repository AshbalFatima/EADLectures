using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models.ViewModels
{
    public class AppointmentVM
    {
        public int? Id { get; set; }


        [Display(Name = "Appointed as")]
        public string? AppointmentAs { get; set; }
        [Required]
        [Display(Name = "Appointed as")]
        public int AppointmentAsId { get; set; }
        [Display(Name = "Basic Scale")]
        public string? AppointmentBPS { get; set; }
        [Required]
        [Display(Name = "Basic Scale")]
        public int AppointmentBPSId { get; set; }
        //public string? AppiontmentLetterNo { get; set; }

        [Display(Name = "Appiontment Letter")]
        public string? AppiontmentLetterURL { get; set; }

        public string? DateOfAppointmentLetter_FOR { get; set; }

        [Display(Name = "Date of Appiontment")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfAppointmentLetter { get; set; }



        public string? DateOfChargeAssumption_FOR { get; set; }
        [Display(Name = "Date of Charge Assumption/Joining")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfChargeAssumption { get; set; }
        [Display(Name = "Other(Please specify)")]
        public string? QuotaOther { get; set; }

        [Display(Name = "Quota (if any)")]
       

        public string? Quota { get; set; }

        [Display(Name = "Whether Joined Through Proper Channel")]
        public bool JoinThroughProperChannel { get; set; }

        [Required]
        [Display(Name = "Quota (if any)")]
        public int QuotaId { get; set; }
        [Display(Name = "Other(Please specify)")]
        public string? ModeOfAppointmentOther { get; set; }
        [Display(Name = "Mode Of Appointment")]
        public string? ModeOfAppointment { get; set; }
        [Required]
        [Display(Name = "Mode Of Appointment")]
        public int ModeOfAppointmentId { get; set; }
        
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public bool? IsVerified { get; set; }
        public string? VerifiedBy { get; set; }

        public int? ServiceHistoryId { get; set; }
        public string? ServiceHistoryText { get; set; }
        

        public AppointmentVM()
        { 
        }
        public AppointmentVM(Appointment a)
        {
            
            this.AppiontmentLetterURL = a.AppiontmentLetterURL;
            this.AppointmentAs = a.AppointmentAs;
            this.AppointmentAsId = a.AppointmentAsId;
            this.AppointmentBPS = a.AppointmentBPS;
            this.AppointmentBPSId = a.AppointmentBPSId;
            this.DateOfAppointmentLetter = a.DateOfAppointmentLetter;
            this.DateOfChargeAssumption = a.DateOfChargeAssumption;
            this.EmployeeId = a.EmployeeId;
            this.Id = a.Id;
            this.IsVerified = a.IsVerified;
            this.ModeOfAppointment = a.ModeOfAppointment;
            this.ModeOfAppointmentId = a.ModeOfAppointmentId;
            this.Quota = a.Quota;
            this.QuotaId = a.QuotaId;
            this.VerifiedBy = a.VerifiedBy;
            this.JoinThroughProperChannel = a.JoinThroughProperChannel;
            this.ServiceHistoryId = a.ServiceHistoryId;
            this.ServiceHistoryText = a.ServiceHistoryText;


            this.ModeOfAppointmentOther = a.ModeOfAppointmentOther;
            this.QuotaOther = a.QuotaOther;
        }
        public Appointment ToModel()
        {
            Appointment ap = new Appointment();

            
            ap.AppiontmentLetterURL = this.AppiontmentLetterURL;
            ap.AppointmentAs = this.AppointmentAs;
            ap.AppointmentAsId = this.AppointmentAsId;
            ap.AppointmentBPS = this.AppointmentBPS;
            ap.AppointmentBPSId = this.AppointmentBPSId;
            ap.DateOfAppointmentLetter = this.DateOfAppointmentLetter;
            ap.DateOfChargeAssumption = this.DateOfChargeAssumption;
            
            ap.EmployeeId = this.EmployeeId;
            
            if(this.Id.HasValue)
                ap.Id = this.Id.Value;

            ap.IsVerified = this.IsVerified;
            ap.ModeOfAppointment = this.ModeOfAppointment;
            ap.ModeOfAppointmentId = this.ModeOfAppointmentId;
            ap.Quota = this.Quota;
            ap.QuotaId = this.QuotaId;
            ap.VerifiedBy = this.VerifiedBy;
            ap.JoinThroughProperChannel = this.JoinThroughProperChannel;
            ap.ServiceHistoryId = this.ServiceHistoryId;
            ap.ServiceHistoryText = this.ServiceHistoryText;
            ap.ModeOfAppointmentOther = this.ModeOfAppointmentOther;
            ap.QuotaOther = this.QuotaOther;
            return ap;
        }

    }
}
