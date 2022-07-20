using Microsoft.EntityFrameworkCore;
using WeightTrack.Data.DbModels;

namespace WeightTrack.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<WeightEntry> WeightEntries { get; set; }
        public DbSet<WeightTarget> WeightTargets { get; set; }
    }
}
