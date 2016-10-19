using PacMan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Domain.Abstract
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> Products { get; }
        Player DeleteProduct(int playerID);

        void AddPlayer(Player player);
    }
}
