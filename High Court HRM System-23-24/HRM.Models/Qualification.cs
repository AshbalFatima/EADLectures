using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class Qualification
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Degree Title")]
        public string QualitifationTitle { get; set; }

       
        
        public int YearOfResult { get; set; }

        
        [Display(Name = "Result Date")]
        public DateTime? ResultDate { get; set; }
        [Required]
        [Display(Name = "Marks Obtain")]
        public double MarksObtained { get; set; }
        [Required]
        [Display(Name = "Total Marks")]

        public double TotalMarks { get; set; }

        [Required]
        [Display(Name = "Marks Type")]

        public string MarksType { get; set; }
        public string? OtherMarkObtain { get; set; }
        public string? OtherMarkTotal { get; set; }

        [Required]
        [Display(Name = "Select Board/University")]

        public string BoardUniversity { get; set; }
        
        public string? Degree_Front_URL { get; set; }
        public string? Degree_Back_URL { get; set; }

        public string? GradeOrDivisionOrPass { get; set; }
        //Before after joining
        public string QualificationTime { get; set; }
        public bool? IsPlacedOnRecord { get; set; }
        public int DegreeId { get; set; }
        public string DegreeName { get; set; }

        [Display(Name = "Other(Please specify)")]
        public string? DegreeNameOther { get; set; }
        public int DegreeLevelId { get; set; }
        public string DegreeLevel { get; set; }

        public bool NOC { get; set; }
        public string? NOCURL { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public bool IsVerified { get; set; }
        public string? VerifiedBy { get; set; }

    }
}
