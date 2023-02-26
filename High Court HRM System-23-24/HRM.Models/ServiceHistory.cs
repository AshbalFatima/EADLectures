using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class ServiceHistory
    {
        public int Id { get; set; }
        public string Designation { get; set; }
        public string PayScale { get; set; }
        [Required]
        [Display(Name = "Pay Scale")]
        public int PayScaleId { get; set; }
        public string Deparment { get; set; }

        public string ServiceType { get; set; }
        public bool JoinThroughProperChannel { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public string? RelievingURL { get; set; }
        public Employee Employee { get; set; }

        public int EmployeeId { get; set; }
        public bool IsVerified { get; set; }
        public string? VerifiedBy { get; set; }

    }
}
