using PacMan.Domain.Entities;
using System.Data.Entity;
namespace PacMan.Domain
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("ScoreDbConnection")
        {
        }
        public DbSet<Player> Players { get; set; }
        
    }
}
