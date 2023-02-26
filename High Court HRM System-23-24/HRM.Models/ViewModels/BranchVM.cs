using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models.ViewModels
{
    public class BranchVM
    {

        public int? Id { get; set; }
        [Display(Name ="Branch Name")]
        [Required]
        public string BranchName { get; set; }

        
        [ForeignKey("ParentBranchId")]
        public virtual Branch? ParentBranch { get; set; }
        [Display(Name = "Parent Branch")]
        public int? ParentBranchId { get; set; }
        public int OrderBy { get; set; }


        public string? ParentClasses { get; set; }
        public string? ChildClasses { get; set; }

        public BranchVM()
        { 
        }
        public BranchVM(Branch b)
        {
            this.Id = b.Id;
            this.BranchName = b.BranchName;
            if(b.ParentBranchId.HasValue)
                this.ParentBranchId=b.ParentBranchId.Value;
            OrderBy = b.OrderBy;
        }
        public Branch ToModel()
        {
            Branch b = new Branch();
            b.BranchName = this.BranchName;
            if (this.ParentBranchId.HasValue)
                b.ParentBranchId = this.ParentBranchId.Value;
            if(this.Id.HasValue)
                b.Id=this.Id.Value;

            b.OrderBy= this.OrderBy;
            return b; 
        }
    }
}
