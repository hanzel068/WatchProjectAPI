using Microsoft.EntityFrameworkCore;
using WatchProjectAPI.Model;

namespace WatchProjectAPI.Data
{
    public class WatchDbContext : DbContext
    {


        public WatchDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Watch> Watch { get; set; }
    }
}
