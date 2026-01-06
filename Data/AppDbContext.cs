using Microsoft.EntityFrameworkCore;
using NewsAggregator.API.Models;

namespace NewsAggregator.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<News> News { get; set; }
    }
}
