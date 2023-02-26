using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.ViewModels
{
    public class CustomNotificationVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Sender { get; set; }
        public string Status { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }


}
