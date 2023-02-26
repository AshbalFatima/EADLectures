using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.ViewModels
{
    public class DegreeLevelVM
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public ICollection<DegreeTitleVM> Degrees { get; set; }
    }
}
