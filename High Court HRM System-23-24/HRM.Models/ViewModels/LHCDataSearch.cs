using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models.ViewModels
{

    public class LHCDataSearch
    {
        public string CNIC { get; set; }
        public string PersonalNumber { get; set; }
        public List<LHCDataVM> SearchedData { get; set; }
    }
}
