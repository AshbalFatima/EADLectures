using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public string? AppointmentAs { get; set; }
        [Required]
        [Display(Name = "Appointment as")]
        public int AppointmentAsId { get; set; }
        public string? AppointmentBPS { get; set; }
        [Required]
        [Display(Name = "Appointment In BPS")]
        public int AppointmentBPSId { get; set; }
        
        public string? AppiontmentLetterURL { get; set; }
        public DateTime? DateOfAppointmentLetter { get; set; }
        [Display(Name ="Date of Charge Assumption/Joining")]
        public DateTime? DateOfChargeAssumption { get; set; }
        public string? QuotaOther { get; set; }
        public string? Quota { get; set; }
        [Required]
        [Display(Name = "Quota (if any)")]
        public int QuotaId { get; set; }
        public string? ModeOfAppointmentOther { get; set; }
        public string? ModeOfAppointment { get; set; }
        [Required]
        [Display(Name = "Mode Of Appointment (if any)")]
        public int ModeOfAppointmentId { get; set; }


        [Display(Name = "Whether Joined Through Proper Channel")]
        public bool JoinThroughProperChannel { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int? ServiceHistoryId { get; set; }
        public string? ServiceHistoryText { get; set; }
        public ServiceHistory LastService { get; set; }
        public bool? IsVerified { get; set; }
        public string? VerifiedBy { get; set; }

    }
}
