using PacMan.Domain.Entities;
using System.Collections.Generic;

namespace PacMan.Domain.Abstract
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> Players { get; }
        Player DeletePlayer(int playerId);

        void AddPlayer(Player player);
    }
}
