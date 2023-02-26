using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apis.Common.Models
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        
    }
}
