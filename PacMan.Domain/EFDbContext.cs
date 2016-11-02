using PacMan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Domain
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("ScoreDbConnection")
        {
        }

        public DbSet<Player> Players { get; set; }
        
    }
}
