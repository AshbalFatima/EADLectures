using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class Branch
    {
        public int Id { get; set; }
        [Required]
        public string BranchName { get; set; }

        [ForeignKey("ParentBranchId")]
        public virtual Branch ParentBranch { get; set; }
        public int? ParentBranchId { get; set; }
        public int OrderBy { get; set; }
    }
}
