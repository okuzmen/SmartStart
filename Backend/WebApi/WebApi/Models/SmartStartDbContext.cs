using System.Data.Entity;

namespace WebApi.Models
{
    public class SmartStartDbContext : DbContext
    {
        public DbSet<Hole> Holes { get; set; }
    }
}