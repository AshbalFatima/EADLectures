using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class CustomNotification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string  Sender { get; set; }
        public int EmployeeId { get; set; }
        public NotificationStatus Status   { get; set; }
        public Employee Employee { get; set; }
    }
    public enum NotificationStatus
    { 
        Unseen,
        Seen,
        Removed,
    }

}
