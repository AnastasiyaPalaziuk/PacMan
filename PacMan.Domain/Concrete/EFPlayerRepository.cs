using PacMan.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using PacMan.Domain.Entities;
using NLog;

namespace PacMan.Domain.Concrete
{
   

    public class EfPlayerRepository : IPlayerRepository
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger(); 
        private readonly EfDbContext _context = new EfDbContext();
        public IEnumerable<Player> Players => _context.Players;

        public Player DeletePlayer(int playerId)
        {
            Player dbEntry = _context.Players
                           .FirstOrDefault(p => p.Id== playerId);

            if (dbEntry != null)
            {
                _context.Players.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void AddPlayer(Player player)
        {
            _log.Trace("Добавление результатов игрока {0} в базу данных", player.Name);
            _context.Players.Add(player);
            _context.SaveChanges();
        }
    }
}
