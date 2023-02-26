using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.ViewModels
{
    public class ServiceHistoryVM
    {
        public int Id { get; set; }
        public string Designation { get; set; }
        public string PayScale { get; set; }
        public int PayScaleId { get; set; }
        public string Deparment { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public EmployeeVM Employee { get; set; }
        public bool IsVerified { get; set; }
        public string VerifiedBy { get; set; }

    }
}
