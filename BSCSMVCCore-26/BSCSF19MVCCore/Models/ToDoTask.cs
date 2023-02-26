using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace BSCSMVCCore.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }

        [Display(Name ="Task Title")]
        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public string? Path { get; set; }


    }
}
