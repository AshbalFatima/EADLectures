using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class DegreeTitle
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int DegreeLevelId { get; set; }
        public DegreeLevel DegreeLevel { get; set; }
    }
}
