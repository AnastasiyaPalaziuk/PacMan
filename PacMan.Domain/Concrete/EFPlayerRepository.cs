using PacMan.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacMan.Domain.Entities;

namespace PacMan.Domain.Concrete
{
    public class EFPlayerRepository : IPlayerRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Player> Products
        {
            get
            {
                return context.Players;
            }
        }

        public Player DeleteProduct(int playerID)
        {
            Player dbEntry = context.Players
                           .FirstOrDefault(p => p.Id== playerID);

            if (dbEntry != null)
            {
                context.Players.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void AddPlayer(Player player)
        {
            context.Players.Add(player);
            context.SaveChanges();
        }
    }
}
