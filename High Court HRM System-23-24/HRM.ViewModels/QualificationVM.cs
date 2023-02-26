using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.ViewModels
{
    public class QualificationVM
    {
        public int Id { get; set; }
        public string DegreeName { get; set; }
        public int YearOfResult { get; set; }
        public DateTime? ResultDate { get; set; }
        public double MarksObtained { get; set; }
        public double TotalMarks { get; set; }

        public int MarksTypeId { get; set; }
        public string MarksType { get; set; }
        public string OtherMarkObtain { get; set; }
        public string OtherMarkTotal { get; set; }

        public string BoardUniversity { get; set; }
        public int BoardUniversityId { get; set; }
        public string? Degree_Front_URL { get; set; }
        public string? Degree_Back_URL { get; set; }

        public string QualificationTime { get; set; }
        public string QualificationTimeId { get; set; }
        //public DegreeTitle Degree { get; set; }

        //public Employee Employee { get; set; }
        public int DegreeId { get; set; }
        public string DegreeTitle { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsVerified { get; set; }
        public string VerifiedBy { get; set; }

    }
}
