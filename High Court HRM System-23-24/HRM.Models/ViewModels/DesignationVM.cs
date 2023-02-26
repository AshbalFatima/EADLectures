using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models.ViewModels
{
    public class DesignationVM
    {
        public int? Id { get; set; }
        [Display(Name ="Designation Title")]
        public string DesignationTitle { get; set; }
        public string? BPSNAME { get; set; }
        [Display(Name = "Pay Scale")]
        public int BPSID { get; set; }
        public DesignationVM() { }
        public DesignationVM(Designation d) {
            this.Id = d.Id;
            this.BPSNAME = d.BPSNAME;
            this.DesignationTitle = d.DesignationTitle;
            this.BPSID= d.BPSID; 
            
        }
        public Designation ToModel()
        {
            Designation d = new Designation();
            if(this.Id.HasValue)
                d.Id = this.Id.Value;
            d.DesignationTitle = this.DesignationTitle;
            d.BPSNAME= this.BPSNAME;
            d.BPSID = this.BPSID;
            return d;
        }
    }
}
