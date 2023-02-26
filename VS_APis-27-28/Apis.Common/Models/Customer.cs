using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apis.Common.Models
{
    public class Customer : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
