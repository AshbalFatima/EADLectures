using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models.ViewModels
{
    public class EmployeeDataVM
    {
        public EmployeeVM Employee { get; set; }
        public List<QualificationVM> Qualifications { get; set; }
        public List<ServiceHistoryVM> ServiceHistories { get; set; }
        public List<AppointmentVM> Appointments { get; set; }
    }
}
