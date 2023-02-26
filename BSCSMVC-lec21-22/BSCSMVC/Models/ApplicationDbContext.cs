using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BSCSMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext():base("koi_b") 
        { 
            //Database.SetInitializer<ApplicationDbContext>(
            //    new CreateDatabaseIfNotExists<ApplicationDbContext>());
            
        
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
   // public class MyDbIntializer : 
}