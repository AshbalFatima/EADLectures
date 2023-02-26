using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories.Interfaces
{
    public interface IApplicationDbContext
    {
        ApplicationDbContext ProvideDb();
    }
}
