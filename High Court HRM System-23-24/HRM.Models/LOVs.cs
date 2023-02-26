using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class LOVs
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public int OrderBy { get; set; }
        public LOV_Type LOV_Type { get; set; }
    }
    public enum LOV_Type
    { 
        Religions,
        BPS,
        Desginations,
        Genders,
        Domiciles,
        BloodGroups,
        Benches,
        Branches,
        QualficationTimes,
        Nationalities,
        MartialStatuses,
        Univerities,
        MarksTypes,
        Quotas, 
        AppointmentModes,
        ServiceTypes


    }
}
