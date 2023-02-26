using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.ViewModels
{
    public class DegreeTitleVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DegreeLevelId { get; set; }
        public string DegreeLevelName { get; set; }

    }
}
