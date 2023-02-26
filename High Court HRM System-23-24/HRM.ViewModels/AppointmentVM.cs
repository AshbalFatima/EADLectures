using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.ViewModels
{
    public class AppointmentVM
    {
        public int Id { get; set; }
        public string AppointmentAs { get; set; }
        public int AppointmentAsId { get; set; }
        public string AppointmentBPS { get; set; }
        public int AppointmentBPSId { get; set; }
        public string AppiontmentLetterNo { get; set; }
        public DateTime? DateOfAppointmentLetter { get; set; }
        public string Quota { get; set; }
        public int QuotaId { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
