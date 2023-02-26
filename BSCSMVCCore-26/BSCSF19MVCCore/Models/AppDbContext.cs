
using Microsoft.EntityFrameworkCore;

namespace BSCSMVCCore.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        
        }
        public DbSet<Models.ToDoTask> Tasks { get; set; }
    }
}
