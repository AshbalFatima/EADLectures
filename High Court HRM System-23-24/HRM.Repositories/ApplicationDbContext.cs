
using HRM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
          
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<ServiceHistory> ServiceHistories { get; set; }
        public DbSet<DegreeLevel> DegreeLevels { get; set; }
        public DbSet<DegreeTitle> DegreeTitles { get; set; }
        public DbSet<Designation> Designations{ get; set; }
        public DbSet<Domicile> Domiciles{ get; set; }
        public DbSet<LOVs> LOVs{ get; set; }
        public DbSet<LHCData> LHCData{ get; set; }
        public DbSet<Branch> Branches { get; set; }
    }
}
