



//using Microsoft.AspNetCore.Identity;



using Microsoft.AspNetCore.Identity;

namespace HRM.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string CNIC { get; set; }
        public string PersonnelNumber { get; set; }
        public string? OTP { get; set; }
        public bool OTP_Verify { get; set; }
        
        public Employee? Employee { get; set; }

    }
}