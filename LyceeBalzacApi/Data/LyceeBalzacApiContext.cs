using Microsoft.EntityFrameworkCore;
using LyceeBalzacApi.data_models;

namespace LyceeBalzacApi.Data
{
    public class LyceeBalzacApiContext : DbContext
    {
        public LyceeBalzacApiContext (DbContextOptions<LyceeBalzacApiContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Level1> Level1 { get; set; }
    }
}
