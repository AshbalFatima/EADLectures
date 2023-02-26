using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models.ViewModels
{
    public class LHCDataVM
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name ="CNIC without Dashes")]
        public string CNIC { get; set; }
        [Display(Name = "Personal Number")]
        [Required]
        public string? PersonalNumber { get; set; }
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }
        [Display(Name = "Father Name")]
        public string? FatherName { get; set; }
        [Display(Name = "Mobile Number")]

        public string? MobileNumber { get; set; }
        public LHCDataVM()
        { 
        }
        public LHCDataVM(LHCData l)
        {

            this.Id = l.Id;
            this.FullName = l.FullName;
            this.FatherName = l.FatherName;
            this.MobileNumber = l.MobileNumber;
            this.PersonalNumber = l.PersonalNumber;
            this.CNIC = l.CNIC;

        }
        public LHCData ToModel()
        {
            LHCData d = new LHCData();
            if(this.Id.HasValue)
                d.Id = this.Id.Value;
            d.FullName = this.FullName;
            d.FatherName = this.FatherName;
            d.MobileNumber = this.MobileNumber;
            d.PersonalNumber = this.PersonalNumber;
            d.CNIC = this.CNIC;
            return d;
        }
    }
}
