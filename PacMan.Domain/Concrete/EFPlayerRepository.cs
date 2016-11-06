using PacMan.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacMan.Domain.Entities;
using NLog;

namespace PacMan.Domain.Concrete
{
   

    public class EFPlayerRepository : IPlayerRepository
    {
        private Logger log = LogManager.GetCurrentClassLogger(); 
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Player> Players
        {
            get
            {
                return context.Players;
            }
        }

        public Player DeletePlayer(int playerID)
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
            log.Trace("Добавление результатов игрока {0} в базу данных", player.Name);
            context.Players.Add(player);
            context.SaveChanges();
        }
    }
}
