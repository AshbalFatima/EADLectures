using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.UI.CLS
{
    public class AppDB : DbContext
    {
        public AppDB() : base("name=conStr")
        { 
            //Default Create database if not exsit
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DoTask> DoTasks { get; set; }
    }
}
