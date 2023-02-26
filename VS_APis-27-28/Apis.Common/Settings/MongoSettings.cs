using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apis.Common.Settings
{
    public class MongoSettings
    {
        public string server { get; set; }
        public int port { get; set; }
        public string ConnectionString => $"mongodb://{server}:{port}";
    }
}
