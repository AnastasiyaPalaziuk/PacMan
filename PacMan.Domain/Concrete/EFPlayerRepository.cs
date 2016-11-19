using PacMan.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using PacMan.Domain.Entities;
using NLog;
using System.Data.Entity.Infrastructure;

namespace PacMan.Domain.Concrete
{


    public class EfPlayerRepository : IPlayerRepository
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();
        private readonly EfDbContext _context = new EfDbContext();
        public IEnumerable<Player> Players => _context.Players;

        public void AddPlayer(Player player)
        {
            try
            {
                _context.Players.Add(player);
                _context.SaveChanges();
                _log.Trace("Add item to database - Successful");
                
            }
            catch (DbUpdateException e)
            {
                _log.Error("Add item to databas - Fail.\nError massege: {0}", e.InnerException.Message);
            }
        }
    }
}
