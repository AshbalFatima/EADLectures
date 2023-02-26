using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models.ViewModels
{
    public class ServiceHistoryVM
    {

        public int? Id { get; set; }
        public string Designation { get; set; }
        public string? PayScale { get; set; }
        [Required]
        [Display(Name = "Pay Scale")]
        public int PayScaleId { get; set; }
        public string Deparment { get; set; }

        public string ServiceType { get; set; }
        [Display(Name ="Join Through Proper Channel")]
        public bool JoinThroughProperChannel { get; set; }

        public string? From_FOR { get; set; }
        [Display(Name = "Join Date")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}" ,ApplyFormatInEditMode =true)]
        
        public DateTime? From { get; set; }
        public string? To_FOR { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Leaving Date")]
        public DateTime? To { get; set; }

        [Display(Name ="Copy of Relieving Letter")]
        public string? RelievingURL { get; set; }
        public Employee? Employee { get; set; }

        public int EmployeeId { get; set; }
        public bool? IsVerified { get; set; }
        public string? VerifiedBy { get; set; }
        public ServiceHistoryVM()
        { }
        public ServiceHistoryVM(ServiceHistory s)
        {
            this.Deparment = s.Deparment;
            this.Designation = s.Designation;
            this.EmployeeId = s.EmployeeId;
            if (s.From.HasValue)
                this.From = s.From.Value;
            
            this.Id = s.Id;
            this.IsVerified = s.IsVerified;
            this.JoinThroughProperChannel = s.JoinThroughProperChannel;
            
            this.PayScale = s.PayScale;
            this.PayScaleId = s.PayScaleId;
            this.ServiceType = s.ServiceType;
            if(s.To.HasValue)
            this.To = s.To.Value;
            this.VerifiedBy = s.VerifiedBy;
            this.IsVerified = s.IsVerified;
            this.RelievingURL = s.RelievingURL;
        }
        public ServiceHistory ToModel()
        {
            ServiceHistory sh = new ServiceHistory();

            sh.Deparment = this.Deparment;
            sh.Designation = this.Designation;
            sh.EmployeeId = this.EmployeeId;

            sh.From = this.From;
            if (this.Id.HasValue)
                sh.Id = this.Id.Value;

            if (this.IsVerified.HasValue)
                sh.IsVerified = this.IsVerified.Value;

            sh.JoinThroughProperChannel = this.JoinThroughProperChannel;

            sh.PayScale = this.PayScale;
            sh.PayScaleId = this.PayScaleId;
            sh.ServiceType = this.ServiceType;
            sh.To = this.To;
            sh.VerifiedBy = this.VerifiedBy;
            if (this.IsVerified.HasValue)
                sh.IsVerified = this.IsVerified.Value;
            sh.RelievingURL = this.RelievingURL;
            return sh;
        }
        
    }
}
