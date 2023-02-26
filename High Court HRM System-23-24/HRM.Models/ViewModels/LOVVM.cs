using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models.ViewModels
{
    public class LOVVM
    {
            public int? Id { get; set; }
            public string Text { get; set; }
            public string? Value { get; set; }
            public int? OrderBy { get; set; }
            public LOV_Type LOV_Type { get; set; }
        public LOVs ToModel() {
            LOVs m = new LOVs();
            if (this.Id.HasValue)
                m.Id = this.Id.Value;
            
            if (this.OrderBy.HasValue)
                m.OrderBy = this.OrderBy.Value;
            m.Value = this.Value;
            m.Text = this.Text;
            m.LOV_Type = this.LOV_Type;
            return m;


        }
        public LOVVM()
        { 
        }
        public LOVVM(LOVs m)
        {


            this.Id = m.Id;


            this.OrderBy = m.OrderBy;
            this.Value = m.Value;
            this.Text = m.Text;
            this.LOV_Type = m.LOV_Type;
        }
    }
}
