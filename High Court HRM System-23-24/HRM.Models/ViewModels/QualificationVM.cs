using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models.ViewModels
{
    public class QualificationVM
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Degree Title")]
        public string QualitifationTitle { get; set; }
        
        [Required]
        [Range(1947,2053)]

        [Display(Name = "Year of Passing")]
        public int YearOfResult { get; set; }

        
        [Display(Name = "Result Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ResultDate { get; set; }
        
        [Required]
        [Display(Name = "Marks Obtained")]
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
        [Display(Name = "University/Board")]

        public string BoardUniversity { get; set; }
        [Display(Name ="Front Image of Degree")]

        public string? Degree_Front_URL { get; set; }
        public string? Degree_Back_URL { get; set; }


        //Before after joining
        [Display(Name = "Degree Obtained")]
        public string QualificationTime { get; set; }

        [Display(Name = "Whether Placed on Record")]
        public bool IsPlacedOnRecord { get; set; }
        [Required]
        [Display(Name = "Degree Name")]
        public int DegreeId { get; set; }
        public string? DegreeName { get; set; }

        [Display(Name = "Other(Please specify)")]
        public string? DegreeNameOther { get; set; }
        [Display(Name ="Degree Level")]
        public int DegreeLevelId { get; set; }
        public string? DegreeLevel { get; set; }
        public int EmployeeId { get; set; }

        [Display(Name ="Result Grade/Division (in case of grading result system)")]
        public string GradeOrDivisionOrPass { get; set; }
        [Display(Name = "Obtained NOC (if yes,upload copy)")]
        public bool NOC { get; set; }
        [Display(Name = "Upload Copy of NOC")]
        public string? NOCURL { get; set; }

        public bool? IsVerified { get; set; }
        public string? VerifiedBy { get; set; }

        public QualificationVM()
        { 
        
        }
        public QualificationVM(Qualification q)
        {
            this.BoardUniversity = q.BoardUniversity;
            this.DegreeId = q.DegreeId;
            this.DegreeLevel = q.DegreeLevel;
            this.DegreeLevelId = q.DegreeLevelId;
            this.DegreeName = q.DegreeName;
            this.Degree_Back_URL = q.Degree_Back_URL;
            this.Degree_Front_URL = q.Degree_Front_URL;
            this.EmployeeId = q.EmployeeId;
            this.Id = q.Id;
            if(q.IsPlacedOnRecord.HasValue)
            this.IsPlacedOnRecord = q.IsPlacedOnRecord.Value;
            this.IsVerified = q.IsVerified;
            this.MarksObtained=q.MarksObtained;
            this.MarksType = q.MarksType;
            this.OtherMarkObtain = q.OtherMarkObtain;
            this.OtherMarkTotal = q.OtherMarkTotal; 
            this.QualificationTime = q.QualificationTime;
            this.QualitifationTitle = q.QualitifationTitle;
            if(q.ResultDate.HasValue)
            this.ResultDate = q.ResultDate.Value;
            this.TotalMarks = q.TotalMarks;
            this.VerifiedBy = q.VerifiedBy;
            this.YearOfResult = q.YearOfResult;
            this.GradeOrDivisionOrPass = q.GradeOrDivisionOrPass;
            this.NOC = q.NOC;
            this.NOCURL = q.NOCURL;
            this.DegreeNameOther = q.DegreeNameOther;
        }

        public Qualification ToModel()
        {
            Qualification tt = new Qualification();
            tt.BoardUniversity = this.BoardUniversity;
            tt.DegreeId = this.DegreeId;
            tt.DegreeLevel = this.DegreeLevel;
            tt.DegreeLevelId = this.DegreeLevelId;
            tt.DegreeName = this.DegreeName;
            tt.Degree_Back_URL = this.Degree_Back_URL;
            tt.Degree_Front_URL = this.Degree_Front_URL;
            tt.EmployeeId = this.EmployeeId;
            
            if (this.Id.HasValue && this.Id.Value>-1)
                tt.Id = this.Id.Value;
            tt.IsPlacedOnRecord = this.IsPlacedOnRecord;
            if(this.IsVerified.HasValue)
            tt.IsVerified = this.IsVerified.Value;
            tt.MarksObtained = this.MarksObtained;
            tt.MarksType = this.MarksType;
            tt.OtherMarkObtain = this.OtherMarkObtain;
            tt.OtherMarkTotal = this.OtherMarkTotal;
            tt.QualificationTime = this.QualificationTime;
            tt.QualitifationTitle = this.QualitifationTitle;
            tt.ResultDate = this.ResultDate;
            tt.TotalMarks = this.TotalMarks;
            tt.VerifiedBy = this.VerifiedBy;
            tt.YearOfResult = this.YearOfResult;
            tt.GradeOrDivisionOrPass = this.GradeOrDivisionOrPass;
            tt.NOC = this.NOC;
            tt.NOCURL = this.NOCURL;
            tt.DegreeNameOther = this.DegreeNameOther;
            return tt;

        }



    }
  
}
