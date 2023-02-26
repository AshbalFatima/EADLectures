using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RP.Pages
{
    public class ContactModel : PageModel
    {

        public ContactModel(IAbstraction obj)
        { 
        
        }




        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }

        [BindProperty] 
        public string Message { get; set; }
        [BindProperty] 
        public string Summary { get; set; }
        public void OnGet()
        {
            //first time when you open the page...
            this.Summary = "Summary will go here!!!";
        }
        public void OnPost()
        {


            this.Summary = string.Format("Email: {0} | Mobile : {1} |Message : {2}"
                , this.Email, this.PhoneNumber, this.Message);
            
        }
    }
}
