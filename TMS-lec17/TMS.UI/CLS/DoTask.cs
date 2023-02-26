using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.UI.CLS
{
    public class DoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string CurrentStatus { get; set; }
        // One task associated with one User
        public User User { get; set; }

    }
}
