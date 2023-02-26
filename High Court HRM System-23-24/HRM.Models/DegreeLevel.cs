using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class DegreeLevel
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public ICollection<DegreeTitle> Degrees { get; set; }
    }
}
