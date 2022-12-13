using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DataFile
{

    // DbContext connects to SQL 
    public class ApplicationDbContext : DbContext
    {
        // Pass values/options in appsettings json file to parent (Entity framework)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Every table in DB will need a corresponding Set
        public DbSet<Category> Categories { get; set; }
    }
}
